using System;
using System.Collections.Generic;
using Xunit;

public class OrderSystemTests
{
    [Fact]
    public void CreateOrder_ShouldInitializeCorrectly()
    {
        var menu = new Menu();
        var manager = new OrderManager(menu);

        var order = manager.CreateOrder(new List<string> { "Pizza", "Salad" }, OrderType.Standard, discount: 10, tax: 0.1, deliveryFee: 5);

        Assert.NotNull(order);
        Assert.Equal(1, order.GetId());
        Assert.Equal(OrderType.Standard, order.GetOrderType());
        Assert.Equal(OrderStatus.Preparing, order.GetStatus());
        Assert.Equal(2, order.GetItems().Count);
        Assert.NotEmpty(order.GetStatusHistory());
    }

    [Fact]
    public void UpdateOrderStatus_ShouldTrackHistory()
    {
        var menu = new Menu();
        var manager = new OrderManager(menu);

        var order = manager.CreateOrder(new List<string> { "Burger" }, OrderType.Express);
        order.UpdateStatus(OrderStatus.Delivering);
        order.UpdateStatus(OrderStatus.Completed);

        var history = order.GetStatusHistory();

        Assert.Equal(3, history.Count);
        Assert.Equal(OrderStatus.Preparing, history[0].Status);
        Assert.Equal(OrderStatus.Delivering, history[1].Status);
        Assert.Equal(OrderStatus.Completed, history[2].Status);
    }

    [Fact]
    public void OrderManager_ShouldReturnOrdersByStatus()
    {
        var menu = new Menu();
        var manager = new OrderManager(menu);

        var order1 = manager.CreateOrder(new List<string> { "Sushi" }, OrderType.Standard);
        var order2 = manager.CreateOrder(new List<string> { "Pasta" }, OrderType.Express);

        order1.UpdateStatus(OrderStatus.Delivering);

        var preparingOrders = manager.GetOrdersByStatus(OrderStatus.Preparing);
        var deliveringOrders = manager.GetOrdersByStatus(OrderStatus.Delivering);

        Assert.Contains(order1, deliveringOrders);
        Assert.Contains(order2, preparingOrders);
    }

    [Fact]
    public void GetAllOrdersWithHistory_ShouldReturnFullOrderTracking()
    {
        var menu = new Menu();
        var manager = new OrderManager(menu);

        var order1 = manager.CreateOrder(new List<string> { "Pizza" }, OrderType.Standard);
        var order2 = manager.CreateOrder(new List<string> { "Salad", "Sushi" }, OrderType.Express);

        order1.UpdateStatus(OrderStatus.Delivering);
        order1.UpdateStatus(OrderStatus.Completed);

        order2.UpdateStatus(OrderStatus.Delivering);

        var allHistory = manager.GetAllOrdersWithHistory();

        Assert.Contains(allHistory, h => h.OrderId == order1.GetId() && h.Status == OrderStatus.Preparing);
        Assert.Contains(allHistory, h => h.OrderId == order1.GetId() && h.Status == OrderStatus.Delivering);
        Assert.Contains(allHistory, h => h.OrderId == order1.GetId() && h.Status == OrderStatus.Completed);
        Assert.Contains(allHistory, h => h.OrderId == order2.GetId() && h.Status == OrderStatus.Preparing);
        Assert.Contains(allHistory, h => h.OrderId == order2.GetId() && h.Status == OrderStatus.Delivering);
    }
}
