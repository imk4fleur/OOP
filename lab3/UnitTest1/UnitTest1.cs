using System;
using System.Collections.Generic;
using Xunit;

public class OrderSystemTests
{
    private Menu menu;
    private OrderManager manager;

    public OrderSystemTests()
    {
        menu = new Menu();
        manager = new OrderManager(menu);
    }

    [Fact]
    public void CreateOrder_ShouldInitializeCorrectly()
    {
        var order = manager.CreateOrder(new List<string> { "Pizza", "Salad" }, OrderType.Standard, discount: 10, tax: 0.1, deliveryFee: 5);
        Assert.NotNull(order);
        Assert.Equal(1, order.GetId());
        Assert.Equal(OrderType.Standard, order.GetOrderType());
        Assert.Equal(OrderStatus.Preparing, order.GetStatus());
        Assert.Equal(2, order.GetItems().Count);
        Assert.NotEmpty(order.GetStatusHistory());
    }

    [Fact]
    public void CreateOrder_ShouldThrowException_IfItemNotInMenu()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
            manager.CreateOrder(new List<string> { "NonExistentDish" }, OrderType.Standard));
        Assert.Equal("Блюдо 'NonExistentDish' отсутствует в меню.", exception.Message);
    }

    [Fact]
    public void CreateOrder_ShouldCalculateTotalPriceCorrectly()
    {
        var order = manager.CreateOrder(new List<string> { "Pizza", "Sushi" }, OrderType.Standard, discount: 20, tax: 0.1, deliveryFee: 5);
        double expectedPrice = ((100 + 80 - 20) * 1.1) + 5;
        Assert.Equal(expectedPrice, order.GetTotalPrice(), 2);
    }

    [Fact]
    public void UpdateOrderStatus_ShouldTrackHistory()
    {
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

    [Fact]
    public void GetOrderById_ShouldReturnCorrectOrder()
    {
        var order = manager.CreateOrder(new List<string> { "Burger" }, OrderType.Personalized);
        var fetchedOrder = manager.GetOrderById(order.GetId());
        Assert.NotNull(fetchedOrder);
        Assert.Equal(order.GetId(), fetchedOrder.GetId());
    }

    [Fact]
    public void GetOrderStatus_ShouldReturnCorrectStatus()
    {
        var order = manager.CreateOrder(new List<string> { "Pasta" }, OrderType.Standard);
        order.UpdateStatus(OrderStatus.Delivering);
        var status = manager.GetOrderStatus(order.GetId());
        Assert.Equal(OrderStatus.Delivering, status);
    }

    [Fact]
    public void Menu_ShouldContainDefaultItems()
    {
        var items = menu.GetAllItems();
        Assert.Contains(items, item => item.Name == "Pizza" && item.Price == 100);
        Assert.Contains(items, item => item.Name == "Burger" && item.Price == 50);
        Assert.Contains(items, item => item.Name == "Sushi" && item.Price == 80);
    }

    [Fact]
    public void GetItemByName_ShouldReturnCorrectItem()
    {
        var item = menu.GetItemByName("Pizza");
        Assert.NotNull(item);
        Assert.Equal("Pizza", item.Name);
        Assert.Equal(100, item.Price);
    }

    [Fact]
    public void GetItemByName_ShouldReturnNull_IfItemNotFound()
    {
        var item = menu.GetItemByName("Steak");
        Assert.Null(item);
    }
}
