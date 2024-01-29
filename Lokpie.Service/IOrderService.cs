using Lokpie.Common.Commands;
using Lokpie.Common.Dtos;
using Lokpie.Common.Enums;
using Lokpie.Common.Queries;
using System;
using System.Collections.Generic;

namespace Lokpie.Service
{
    public interface IOrderService
    {
        #region Tenant
        long AddNewTenanProfile(AddTenantCommand command);
        long UpdateTenanProfile(UpdateTenantCommand command);
        IList<TenantDto> GetAllTenant();
        void DeleteTenant(long id);
        #endregion

        #region FoodNBev
        long AddNewFoodNBev(AddFoodNBevCommand command);
        long UpdateFoodNBev(UpdateFoodNBevCommand command);
        IList<FoodNBevDto> GetAllFoodNBev(FoodNBevQuery query);
        BasePhotoDto GetFoodNBevPhoto(long menuId);
        FoodNBevDetailDto GetFoodNBevDetail(long menuId);
        void DeleteFoodNBev(long id);
        #endregion

        #region Table
        long AddTable(AddTableCommand command);
        long UpdateTable(UpdateTableCommand command);
        IList<TableDto> GetAllTable(TableQuery query);
        BasePhotoDto GetTablePhoto(long id);
        TableDetailDto GetTableDetail(long id);
        void DeleteTable(long id);
        #endregion

        #region Order
        long AddNewOrder(AddNewOrderCommand command);
        void UpdateOrderStatus(long orderId, OrderStatus status);
        IList<OrderDto> GetOrderList(OrderStatus? status, long? tableId, DateTime? startDate, DateTime? endDate);
        IList<OrderDetailDto> GetOrderDetail(long orderId);
        #endregion

        #region Reservation
        long AddNewReservation(AddNewReservationCommand command);
        IList<ReservationDto> GetReservationList(ReservationQuery query);
        void UpdateReservationStatus(long reservationId, ReservationStatus status);
        #endregion

        void SetRankOrder();
        IList<HotMenuRankDto> GetHotMenuRanks(DateTime startDate, DateTime endDate);
    }
}
