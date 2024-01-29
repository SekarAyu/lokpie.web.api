using AutoMapper;
using Lokpie.Common.Commands;
using Lokpie.Common.Dtos;
using Lokpie.Common.Enums;
using Lokpie.Common.Queries;
using Lokpie.Repository;
using Lokpie.Repository.Models;
using Newtonsoft.Json;
using QSI.Common.Exception.Common;
using QSI.Document.Common.Commands;
using QSI.Document.Common.Dtos;
using QSI.Document.Service;
using QSI.Persistence.Query;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lokpie.Service.Impl
{
    public class OrderServiceImpl : IOrderService
    {
        private readonly IMapper mapper;
        private readonly ITenantDao tenantDao;
        private readonly ITenantTableDao tenantTableDao;
        private readonly IFoodNBevDao foodNBevDao;
        private readonly IOrderDao orderDao;
        private readonly IOrderDetailDao orderDetailDao;
        private readonly IReservationDao reservationDao;
        private readonly IHotMenuRankDao hotMenuRankDao;
        private readonly IContentService contentService;
        private readonly ConditionFactory conditionFactory;

        public OrderServiceImpl(IMapper mapper, ITenantDao tenantDao, ITenantTableDao tenantTableDao, IFoodNBevDao foodNBevDao, IContentService contentService, ConditionFactory conditionFactory, IOrderDao orderDao, IOrderDetailDao orderDetailDao, IReservationDao reservationDao, IHotMenuRankDao hotMenuRankDao)
        {
            this.mapper = mapper;
            this.tenantDao = tenantDao;
            this.tenantTableDao = tenantTableDao;
            this.foodNBevDao = foodNBevDao;
            this.contentService = contentService;
            this.conditionFactory = conditionFactory;
            this.orderDao = orderDao;
            this.orderDetailDao = orderDetailDao;
            this.reservationDao = reservationDao;
            this.hotMenuRankDao = hotMenuRankDao;
        }

        #region Tenant
        public long AddNewTenanProfile(AddTenantCommand command)
        {
            Tenant newTenant = mapper.Map<Tenant>(command);
            newTenant.IsDeleted = false;
            if (command.Logo != null)
            {
                byte[] imageBytes = Convert.FromBase64String(command.Logo);
                File.WriteAllBytes($"C:\\Publish\\Lokpie_Assets\\{command.LogoFileName}", imageBytes);
                newTenant.Logo = $"C:\\Publish\\Lokpie_Assets\\{command.LogoFileName}";
            }
            var tenant = tenantDao.Save(newTenant);

            return tenant.Id;
        }

        public long UpdateTenanProfile(UpdateTenantCommand command)
        {
            var tenant = tenantDao.Get(command.Id);
            if(tenant != null)
            {
                tenant.Name = command.Name;
                tenant.Address = command.Address;
                tenant.Phone = command.Phone;
                tenant.Email = command.Email;
                tenant.LogoFileName = command.LogoFileName;
                tenant.Type = command.Type;
                if (command.Logo != null)
                {
                    if (tenant.Logo != null)
                        File.Delete(tenant.Logo);
                    byte[] imageBytes = Convert.FromBase64String(command.Logo);
                    File.WriteAllBytes($"C:\\Publish\\Lokpie_Assets\\{command.LogoFileName}", imageBytes);
                    tenant.Logo = $"C:\\Publish\\Lokpie_Assets\\{command.LogoFileName}";
                }
                tenantDao.Save(tenant);
            }

            return command.Id;
        }
        
        public IList<TenantDto> GetAllTenant()
        {
            return mapper.Map<List<TenantDto>>(tenantDao.GetAll());
        }

        public void DeleteTenant(long id)
        {
            var tenant = tenantDao.Get(id);
            if (tenant != null)
            {
                tenant.IsDeleted = true;
                tenantDao.Save(tenant);
            }
        }

        #endregion

        #region Food n Bev
        public long AddNewFoodNBev(AddFoodNBevCommand command)
        {
            FoodNBev newFoodNBev = mapper.Map<FoodNBev>(command);
            Tenant tenant = tenantDao.Get(command.TenantId);
            if(tenant != null)
            {
                newFoodNBev.Tenant = tenant;
            }
            
            if(command.Photo != null)
            {
                byte[] imageBytes = Convert.FromBase64String(command.Photo);
                File.WriteAllBytes($"C:\\Publish\\Lokpie_Assets\\{command.FileName}", imageBytes);
                newFoodNBev.Photo = $"C:\\Publish\\Lokpie_Assets\\{command.FileName}";
            }
            
            newFoodNBev.IsDeleted = false;
            var foodNBev = foodNBevDao.Save(newFoodNBev);

            return foodNBev.Id;
        }

        public long UpdateFoodNBev(UpdateFoodNBevCommand command)
        {
            var foodNBev = foodNBevDao.Get(command.Id);
            if (foodNBev != null)
            {
                foodNBev.IsDeleted = false;
                foodNBev.Description = command.Description;
                foodNBev.Name = command.Name;
                foodNBev.Price = command.Price;
                foodNBev.FileName = command.FileName;
                foodNBev.Stock = command.Stock;
                foodNBev.Type = command.Type;
                foodNBev.Status = command.Status;
                foodNBev.Category = command.Category;
                foodNBev.Discount = command.Discount;
                if(command.Photo != null)
                {
                    if(foodNBev.Photo != null)
                        File.Delete(foodNBev.Photo);
                    byte[] imageBytes = Convert.FromBase64String(command.Photo);
                    File.WriteAllBytes($"C:\\Publish\\Lokpie_Assets\\{command.FileName}", imageBytes);
                    foodNBev.Photo = $"C:\\Publish\\Lokpie_Assets\\{command.FileName}";
                }
                foodNBevDao.Save(foodNBev);
            }

            return command.Id;
        }

        public IList<FoodNBevDto> GetAllFoodNBev(FoodNBevQuery query)
        {
            ICondition condition = conditionFactory.Create();
            if (query.tenantId != null && query.tenantId != 0)
                condition.Column("Tenant.Id").Equal(query.tenantId.Value);
            if (query.type.HasValue)
                condition.Column("Type").Equal(query.type.Value);
            if (query.category.HasValue)
                condition.Column("Category").Equal(query.category.Value);
            if (query.Status.HasValue)
                condition.Column("Status").Equal(query.Status.Value);
            condition.Column("IsDeleted").Equal(false);
            return mapper.Map<List<FoodNBevDto>>(foodNBevDao.GetFoodNBev(condition));
        }

        public BasePhotoDto GetFoodNBevPhoto(long menuId)
        {
            BasePhotoDto basePhoto = new BasePhotoDto();
            var foodnBev = foodNBevDao.Get(menuId);
            if(foodnBev != null && foodnBev.Photo != null)
            {
                byte[] imageBytes = File.ReadAllBytes(foodnBev.Photo);
                basePhoto.Photo = Convert.ToBase64String(imageBytes);
                basePhoto.FileName = foodnBev.FileName;
            }
            return basePhoto;
        }

        public FoodNBevDetailDto GetFoodNBevDetail(long menuId)
        {
            var foodnBev = foodNBevDao.Get(menuId);
            FoodNBevDetailDto foodNBevDetail = mapper.Map<FoodNBevDetailDto>(foodNBevDao.Get(menuId));
            if (foodnBev != null && foodnBev.Photo != null)
            {
                byte[] imageBytes = File.ReadAllBytes(foodnBev.Photo);
                foodNBevDetail.Photo = Convert.ToBase64String(imageBytes);
            }
            return foodNBevDetail;
        }

        public void DeleteFoodNBev(long id)
        {
            var foodNBev = foodNBevDao.Get(id);
            if (foodNBev != null)
            {
                foodNBev.IsDeleted = true;
                foodNBevDao.Save(foodNBev);
            }
        }

        #endregion

        #region Table
        public long AddTable(AddTableCommand command)
        {
            var tenant = tenantDao.Get(command.TenantId);
            if(tenant != null)
            {
                TenantTable newTenantTable = mapper.Map<TenantTable>(command);
                newTenantTable.Tenant = tenant;
                newTenantTable.IsDeleted = false;
                if (command.TablePhoto != null)
                {
                    byte[] imageBytes = Convert.FromBase64String(command.TablePhoto);
                    File.WriteAllBytes($"C:\\Publish\\Lokpie_Assets\\{command.TablePhotoFileName}", imageBytes);
                    newTenantTable.TablePhoto = $"C:\\Publish\\Lokpie_Assets\\{command.TablePhotoFileName}";
                }
                if (command.QrCode != null)
                {
                    byte[] imageBytes = Convert.FromBase64String(command.QrCode);
                    File.WriteAllBytes($"C:\\Publish\\Lokpie_Assets\\{command.QrCodeFileName}", imageBytes);
                    newTenantTable.QrCode = $"C:\\Publish\\Lokpie_Assets\\{command.QrCodeFileName}";
                }
                var tenantTable = tenantTableDao.Save(newTenantTable);
                return tenantTable.Id;
            }

            return 0;
        }

        public long UpdateTable(UpdateTableCommand command)
        {
            var tenantTable = tenantTableDao.Get(command.Id);
            var tenant = tenantDao.Get(command.TenantId);
            if (tenantTable != null && tenant != null)
            {
                tenantTable.AreaName = command.AreaName;
                tenantTable.Description = command.Description;
                tenantTable.Number = command.Number;
                tenantTable.Capacity = command.Capacity;
                tenantTable.Status = command.Status;
                tenantTable.ReservationType = command.ReservationType;
                if (command.QrCode != null)
                {
                    if (tenantTable.QrCode != null)
                        File.Delete(tenantTable.QrCode);
                    byte[] imageBytes = Convert.FromBase64String(command.QrCode);
                    File.WriteAllBytes($"C:\\Publish\\Lokpie_Assets\\{command.QrCodeFileName}", imageBytes);
                    tenantTable.QrCode = $"C:\\Publish\\Lokpie_Assets\\{command.QrCodeFileName}";
                }
                if (command.TablePhoto != null)
                {
                    if (tenantTable.TablePhoto != null)
                        File.Delete(tenantTable.TablePhoto);
                    byte[] imageBytes = Convert.FromBase64String(command.TablePhoto);
                    File.WriteAllBytes($"C:\\Publish\\Lokpie_Assets\\{command.TablePhotoFileName}", imageBytes);
                    tenantTable.TablePhoto = $"C:\\Publish\\Lokpie_Assets\\{command.TablePhotoFileName}";
                }

                tenantTableDao.Save(tenantTable);
            }

            return command.Id;
        }

        public IList<TableDto> GetAllTable(TableQuery query)
        {
            IList<TableDto> tableDtos = new List<TableDto>();
            ICondition condition = conditionFactory.Create();
            if (query.Status.HasValue)
                condition.Column("Status").Equal(query.Status.Value);

            tableDtos = mapper.Map<List<TableDto>>(tenantTableDao.GetTenantTable(condition));
            IList<TableDto> fixTableDtos = new List<TableDto>();
            if (query.AvailableDate.HasValue)
            {
                foreach(var item in tableDtos)
                {
                    if(item.TableReservationDate.Count() > 0 && item.TableReservationDate.Where(x => x.StartsWith(query.AvailableDate.Value.ToString("d"))).Count() == 0)
                        fixTableDtos.Add(item);
                    else if(item.TableReservationDate == null || item.TableReservationDate.Count() == 0)
                        fixTableDtos.Add(item);
                }
            }
            else
                fixTableDtos = tableDtos;
                
            return fixTableDtos;
        }

        public BasePhotoDto GetTablePhoto(long id)
        {
            BasePhotoDto basePhoto = new BasePhotoDto();
            var tenantTable = tenantTableDao.Get(id);
            if (tenantTable != null && tenantTable.TablePhoto != null)
            {
                byte[] imageBytes = File.ReadAllBytes(tenantTable.TablePhoto);
                basePhoto.Photo = Convert.ToBase64String(imageBytes);
                basePhoto.FileName = tenantTable.TablePhotoFileName;
            }
            return basePhoto;
        }

        public TableDetailDto GetTableDetail(long id)
        {
            var tenantTable = tenantTableDao.Get(id);
            TableDetailDto tenantTableDetail = mapper.Map<TableDetailDto>(tenantTable);
            if (tenantTable != null && tenantTable.TablePhoto != null)
            {
                byte[] imageBytes = File.ReadAllBytes(tenantTable.TablePhoto);
                tenantTableDetail.TablePhoto = Convert.ToBase64String(imageBytes);
            }
            return tenantTableDetail;
        }

        public void DeleteTable(long id)
        {
            var tenantTable = tenantTableDao.Get(id);
            if (tenantTable != null)
            {
                tenantTable.IsDeleted = true;
                tenantTableDao.Save(tenantTable);
            }
        }
        #endregion

        #region Order

        public long AddNewOrder(AddNewOrderCommand command)
        {
            var table = tenantTableDao.Get(command.TableId);
            if(table != null && command.OrderDetails != null && command.OrderDetails.Count > 0)
            {
                foreach (var item in command.OrderDetails)
                {
                    var menu = foodNBevDao.Get(item.MenuId);
                    if (menu != null && item.Qty > menu.Stock)
                    {
                        throw new CommonException("805", "qty.not.available", $"menu {menu.Name} stock limit with {menu.Stock}");
                    }
                }

                Order order = new Order()
                {
                    OrderDate = DateTime.Now,
                    Status = OrderStatus.Sent,
                    Table = table,
                    UserName = command.UserName,
                    TotalPrice = command.OrderDetails.Sum(x => x.SubTotal),
                    PaymentMethod = command.PaymentMethod
                };
                var savedOrder = orderDao.Save(order);

                foreach(var item in command.OrderDetails)
                {
                    var menu = foodNBevDao.Get(item.MenuId);
                    if( menu != null)
                    {
                        OrderDetail orderDetail = new OrderDetail()
                        {
                            Menu = menu,
                            Order = savedOrder,
                            Price = item.Price,
                            Qty = item.Qty,
                            SubTotal = item.SubTotal,
                            Note = item.Note
                        };

                        orderDetailDao.Save(orderDetail);

                        menu.Stock -= (int)item.Qty;
                        foodNBevDao.Save(menu);
                    }
                }
                table.Status = TableStatus.Occupied;
                tenantTableDao.Save(table);
                return savedOrder.Id;
            }
            return 0;
        }

        public void UpdateOrderStatus(long orderId, OrderStatus status)
        {
            var order = orderDao.Get(orderId);
            if(order != null)
            {
                order.Status = status;
                orderDao.Save(order);
                if(status == OrderStatus.Finish)
                {
                    var table = tenantTableDao.Get(order.Table.Id);
                    if(table != null)
                    {
                        table.Status = TableStatus.Available;
                        tenantTableDao.Save(table);
                    }
                }
            }
        }

        public IList<OrderDto> GetOrderList(OrderStatus? status, long? tableId, DateTime? startDate, DateTime? endDate)
        {
            IList<OrderDto> orderDtos = new List<OrderDto>();
            ICondition condition = conditionFactory.Create();
            if (status.HasValue)
                condition.Column("Status").Equal(status.Value);
            if(tableId.HasValue)
                condition.Column("Table.Id").Equal(tableId.Value);
            if(startDate.HasValue && endDate.HasValue)
                condition.Column("OrderDate").Before(new DateTime(endDate.Value.Year, endDate.Value.Month, endDate.Value.Day, 23, 59, 59)).And.Column("OrderDate").After(new DateTime(startDate.Value.Year, startDate.Value.Month, startDate.Value.Day, 0, 0, 0));
            orderDtos = mapper.Map<List<OrderDto>>(orderDao.GetOrder(condition));
            return orderDtos;
        }

        public IList<OrderDetailDto> GetOrderDetail(long orderId)
        {
            IList<OrderDetailDto> orderDtos = new List<OrderDetailDto>();
            ICondition condition = conditionFactory.Create();
            condition.Column("Order.Id").Equal(orderId);
            orderDtos = mapper.Map<List<OrderDetailDto>>(orderDetailDao.GetOrderDetail(condition));
            return orderDtos;
        }

        #endregion

        #region reservation

        public long AddNewReservation(AddNewReservationCommand command)
        {
            var table = tenantTableDao.Get(command.TableId);
            if (table != null)
            {
                Reservation reservation = mapper.Map<Reservation>(command);
                reservation.Table = table;
                reservation.Status = ReservationStatus.New;
                long orderId = 0;
                if(command.OrderDetails != null && command.OrderDetails.Count > 0)
                {
                    AddNewOrderCommand newOrderCommand = new AddNewOrderCommand()
                    {
                        TableId = command.TableId,
                        UserName = command.UserName,
                        OrderDetails = command.OrderDetails,
                    };
                    orderId = AddNewOrder(newOrderCommand);
                }
                reservation.OrderId = orderId;
                var savedReserv = reservationDao.Save(reservation);

                IList<string> dates = new List<string>();
                if (!string.IsNullOrWhiteSpace(table.TableReservationDate))
                    dates = JsonConvert.DeserializeObject<IList<string>>(table.TableReservationDate);

                dates.Add(command.OrderDate.ToString("g"));
                table.TableReservationDate = JsonConvert.SerializeObject(dates);
                tenantTableDao.Save(table);

                return savedReserv.Id;
            }
            return 0;
        }

        public IList<ReservationDto> GetReservationList(ReservationQuery query)
        {
            IList<ReservationDto> ReservationDtos = new List<ReservationDto>();
            ICondition condition = conditionFactory.Create();
            if (query.status.HasValue)
                condition.Column("Status").Equal(query.status.Value);
            if (query.startDate.HasValue && query.endDate.HasValue)
                condition.Column("OrderDate").Before(new DateTime(query.endDate.Value.Year, query.endDate.Value.Month, query.endDate.Value.Day, 23, 59, 59)).And.Column("OrderDate").After(new DateTime(query.startDate.Value.Year, query.startDate.Value.Month, query.startDate.Value.Day, 0, 0, 0));
            if(!string.IsNullOrWhiteSpace(query.username))
                condition.Column("UserName").Equal(query.username);
            ReservationDtos = mapper.Map<List<ReservationDto>>(reservationDao.GetReservation(condition));
            return ReservationDtos;
        }

        public void UpdateReservationStatus(long reservationId, ReservationStatus status)
        {
            var reservation = reservationDao.Get(reservationId);
            if (reservation != null)
            {
                reservation.Status = status;
                reservationDao.Save(reservation);
                if (status == ReservationStatus.Reject)
                {
                    var table = tenantTableDao.Get(reservation.Table.Id);
                    if (table != null && table.TableReservationDate != null)
                    {
                        IList<string> dates = new List<string>();
                        if (!string.IsNullOrWhiteSpace(table.TableReservationDate))
                            dates = JsonConvert.DeserializeObject<IList<string>>(table.TableReservationDate);

                        if(dates != null && dates.Count > 0)
                        {
                            var dateReserv = dates.Where(x => x == reservation.OrderDate.ToString("g")).ToList();
                            if (dateReserv != null && dateReserv.Count > 0)
                                dates.Remove(reservation.OrderDate.ToString("g"));
                        }

                        table.TableReservationDate = JsonConvert.SerializeObject(dates);
                        tenantTableDao.Save(table);
                    }
                }
            }
        }

        #endregion

        #region hot menu rank
        public void SetRankOrder()
        {
            var rankDate = DateTime.Now.AddDays(-1);
            var startRankDate = DateTime.Now.AddDays(-31);
            ICondition condition = conditionFactory.Create();
            condition.Column("Timestamp").Before(new DateTime(rankDate.Year, rankDate.Month, rankDate.Day, 23, 59, 59)).And.Column("Timestamp").After(new DateTime(startRankDate.Year, startRankDate.Month, startRankDate.Day, 0, 0, 0));
            var orderlist = orderDetailDao.GetOrderDetail(condition);
            IDictionary<long, long> listIdMenu = new Dictionary<long, long>();
            if (orderlist != null && orderlist.Count > 0)
            {
                var groupedKeys = orderlist.GroupBy(x => x.Menu.Id)
                    .Select(g => new // project each group into a new object
                    {
                        Id = g.Key, // the key of the group is the category
                        Qty = g.Sum(p => p.Qty) // the sum of the prices in the group
                    });

                foreach(var item in groupedKeys)
                {
                    listIdMenu.Add(item.Id, item.Qty);
                }
            }
            var resultRank = listIdMenu.OrderByDescending(x => x.Value);

            HotMenuRank hotMenuRank = new HotMenuRank()
            {
                RankDate = rankDate,
                FirstId = resultRank.ElementAt(0).Key,
                FirstQty = resultRank.ElementAt(0).Value,
                SecondId = resultRank.ElementAt(1).Key,
                SecondQty = resultRank.ElementAt(1).Value,
                ThirdId = resultRank.ElementAt(2).Key,
                ThirdQty = resultRank.ElementAt(2).Value,
            };
            hotMenuRankDao.Save(hotMenuRank);
        }

        public IList<HotMenuRankDto> GetHotMenuRanks(DateTime startDate, DateTime endDate)
        {
            IList<HotMenuRankDto> hotMenuRankDtos = new List<HotMenuRankDto>();
            ICondition condition = conditionFactory.Create();
            condition.Column("RankDate").Before(new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59)).And.Column("RankDate").After(new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0));
            hotMenuRankDtos = mapper.Map<List<HotMenuRankDto>>(hotMenuRankDao.GetHotMenuRank(condition));
            return hotMenuRankDtos;
        }
        #endregion
    }
}
