using Lokpie.Common.Commands;
using Lokpie.Common.Dtos;
using Lokpie.Common.Enums;
using Lokpie.Common.Queries;
using Lokpie.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Authentication;

namespace Lokpie.Api.AspNetCore.Controller
{
    [Authorize]
    [ApiController]
    [AllowAnonymous]
    [Route("Lokpie")]
    public class LokpieContorller : ControllerBase
    {
        private readonly IOrderService orderService;

        public LokpieContorller(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        /// <summary>
        /// Get all tenant
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("tenant")]
        [ProducesResponseType(typeof(IList<TenantDto>), 200)]
        public ActionResult<IList<TenantDto>> GetAllTenant()
        {
            return Ok(orderService.GetAllTenant());
        }

        /// <summary>
        /// Add new Tenant
        /// </summary>
        /// <param name="File"></param>
        /// <param name="command"></param>
        /// <exception cref="AuthenticationException"></exception>
        [HttpPost]
        [Route("tenant")]
        [ProducesResponseType(typeof(long), 200)]
        public long AddNewTenanProfile([FromBody] AddTenantCommand command)
        {
            return orderService.AddNewTenanProfile(command);
        }

        /// <summary>
        /// Update Tenant
        /// </summary>
        /// <param name="command"></param>
        /// <exception cref="AuthenticationException"></exception>
        [HttpPut]
        [Route("tenant")]
        [ProducesResponseType(typeof(long), 200)]
        public long UpdateTenanProfile([FromForm] IFormFile File, [FromBody]UpdateTenantCommand command)
        {
            return orderService.UpdateTenanProfile(command);
        }

        /// <summary>
        /// Update Tenant
        /// </summary>
        /// <exception cref="AuthenticationException"></exception>
        [HttpDelete]
        [Route("tenant")]
        [ProducesResponseType(typeof(long), 200)]
        public void DeleteTenant([FromQuery]long id)
        {
            orderService.DeleteTenant(id);
        }
        
        /// <summary>
        /// Get all menu
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("menu")]
        [ProducesResponseType(typeof(IList<FoodNBevDto>), 200)]
        public ActionResult<IList<FoodNBevDto>> GetAllFoodNBev([FromQuery] FoodNBevQuery query)
        {
            return Ok(orderService.GetAllFoodNBev(query));
        }
        
        /// <summary>
        /// Get menu detail
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("menu/detail")]
        [ProducesResponseType(typeof(FoodNBevDetailDto), 200)]
        public ActionResult<FoodNBevDetailDto> GetFoodNBevDetail(long menuId)
        {
            return Ok(orderService.GetFoodNBevDetail(menuId));
        }
        
        /// <summary>
        /// Get menu photo
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("menu/photo")]
        [ProducesResponseType(typeof(BasePhotoDto), 200)]
        public ActionResult<BasePhotoDto> GetFoodNBevPhoto([FromQuery]long idMenu)
        {
            return Ok(orderService.GetFoodNBevPhoto(idMenu));
        }

        /// <summary>
        /// Add new menu
        /// </summary>
        /// <param name="command"></param>
        /// <exception cref="AuthenticationException"></exception>
        [HttpPost]
        [Route("menu")]
        [ProducesResponseType(typeof(long), 200)]
        public long AddNewFoodNBev([FromBody] AddFoodNBevCommand command)
        {
            return orderService.AddNewFoodNBev(command);
        }

        /// <summary>
        /// Update menu
        /// </summary>
        /// <param name="command"></param>
        /// <exception cref="AuthenticationException"></exception>
        [HttpPut]
        [Route("menu")]
        [ProducesResponseType(typeof(long), 200)]
        public long UpdateFoodNBev([FromBody] UpdateFoodNBevCommand command)
        {
            return orderService.UpdateFoodNBev(command);
        }

        /// <summary>
        /// Update menu
        /// </summary>
        /// <exception cref="AuthenticationException"></exception>
        [HttpDelete]
        [Route("menu")]
        [ProducesResponseType(typeof(long), 200)]
        public void DeleteFoodNBev([FromQuery]long id)
        {
            orderService.DeleteFoodNBev(id);
        }
        
        /// <summary>
        /// Get all table
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("table")]
        [ProducesResponseType(typeof(IList<TenantDto>), 200)]
        public ActionResult<IList<TenantDto>> GetAllTable([FromQuery] TableQuery query)
        {
            return Ok(orderService.GetAllTable(query));
        }
        
        /// <summary>
        /// Get table photo
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("table/photo")]
        [ProducesResponseType(typeof(BasePhotoDto), 200)]
        public ActionResult<BasePhotoDto> GetTablePhoto([FromQuery] long id)
        {
            return Ok(orderService.GetTablePhoto(id));
        }
        
        /// <summary>
        /// Get table detail
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("table/detail")]
        [ProducesResponseType(typeof(TableDetailDto), 200)]
        public ActionResult<TableDetailDto> GetTableDetail([FromQuery] long id)
        {
            return Ok(orderService.GetTableDetail(id));
        }

        /// <summary>
        /// Add new table
        /// </summary>
        /// <param name="command"></param>
        /// <exception cref="AuthenticationException"></exception>
        [HttpPost]
        [Route("table")]
        [ProducesResponseType(typeof(long), 200)]
        public long AddTable([FromBody] AddTableCommand command)
        {
            return orderService.AddTable(command);
        }

        /// <summary>
        /// Update table
        /// </summary>
        /// <param name="command"></param>
        /// <exception cref="AuthenticationException"></exception>
        [HttpPut]
        [Route("table")]
        [ProducesResponseType(typeof(long), 200)]
        public long UpdateTable([FromBody] UpdateTableCommand command)
        {
            return orderService.UpdateTable(command);
        }

        /// <summary>
        /// Update table
        /// </summary>
        /// <exception cref="AuthenticationException"></exception>
        [HttpDelete]
        [Route("table")]
        [ProducesResponseType(typeof(long), 200)]
        public void DeleteTable([FromQuery]long id)
        {
            orderService.DeleteTable(id);
        }

        /// <summary>
        /// Add new order
        /// </summary>
        /// <param name="command"></param>
        /// <exception cref="AuthenticationException"></exception>
        [HttpPost]
        [Route("order")]
        [ProducesResponseType(typeof(long), 200)]
        public long AddNewOrder([FromBody]AddNewOrderCommand command)
        {
            return orderService.AddNewOrder(command);
        }

        /// <summary>
        /// Update order status
        /// </summary>
        /// <param name="command"></param>
        /// <exception cref="AuthenticationException"></exception>
        [HttpPut]
        [Route("order/status")]
        [ProducesResponseType(typeof(long), 200)]
        public void UpdateOrderStatus([FromQuery]long orderId, [FromQuery] OrderStatus status)
        {
            orderService.UpdateOrderStatus(orderId, status);
        }

        /// <summary>
        /// Get all order
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("order")]
        [ProducesResponseType(typeof(IList<OrderDto>), 200)]
        public ActionResult<IList<OrderDto>> GetOrderList([FromQuery] OrderStatus? status, [FromQuery] long? tableId, [FromQuery]DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            return Ok(orderService.GetOrderList(status, tableId, startDate, endDate));
        }

        /// <summary>
        /// Get detail order
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("order/detail")]
        [ProducesResponseType(typeof(IList<OrderDetailDto>), 200)]
        public ActionResult<IList<OrderDetailDto>> GetOrderDetail([FromQuery] long orderId)
        {
            return Ok(orderService.GetOrderDetail(orderId));
        }

        /// <summary>
        /// Add new reservation
        /// </summary>
        /// <param name="command"></param>
        /// <exception cref="AuthenticationException"></exception>
        [HttpPost]
        [Route("reservation")]
        [ProducesResponseType(typeof(long), 200)]
        public long AddNewReservation([FromBody]AddNewReservationCommand command)
        {
            return orderService.AddNewReservation(command);
        }

        /// <summary>
        /// Get all reservation
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("reservation")]
        [ProducesResponseType(typeof(IList<ReservationDto>), 200)]
        public ActionResult<IList<ReservationDto>> GetReservationList([FromQuery] ReservationQuery query)
        {
            return Ok(orderService.GetReservationList(query));
        }

        /// <summary>
        /// Update reservation status
        /// </summary>
        /// <param name="command"></param>
        /// <exception cref="AuthenticationException"></exception>
        [HttpPut]
        [Route("reservation/status")]
        [ProducesResponseType(typeof(long), 200)]
        public void UpdateReservationStatus([FromQuery]long reservationId, [FromQuery] ReservationStatus status)
        {
            orderService.UpdateReservationStatus(reservationId, status);
        }

        /// <summary>
        /// Get all rank by date
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("hot/menu")]
        [ProducesResponseType(typeof(IList<HotMenuRankDto>), 200)]
        public ActionResult<IList<HotMenuRankDto>> GetHotMenuRanks([FromQuery]DateTime startDate, [FromQuery]DateTime endDate)
        {
            return Ok(orderService.GetHotMenuRanks(startDate, endDate));
        }
        
        /// <summary>
        /// Get all rank of the day
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("hot/menu/ofTheDay")]
        [ProducesResponseType(typeof(IList<HotMenuRankDto>), 200)]
        public ActionResult<IList<HotMenuRankDto>> GetHotMenuRanks()
        {
            DateTime startDate = DateTime.Now.AddDays(-1);
            DateTime endDate = DateTime.Now.AddDays(-1);
            return Ok(orderService.GetHotMenuRanks(startDate, endDate));
        }
    }
}
