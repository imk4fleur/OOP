using System;
using System.Collections.Generic;
using System.Linq;

public class OrderManager
{
    private List<Order> orders = new List<Order>();
    private Menu menu;
    private int nextOrderId = 1;

    public OrderManager(Menu menu)
    {
        this.menu = menu;
    }

    public Order CreateOrder(List<string> itemNames, OrderType type, double discount = 0, double tax = 0, double deliveryFee = 0)
    {
        var items = new List<MenuItem>();

        foreach (var name in itemNames)
        {
            var item = menu.GetItemByName(name);
            if (item == null)
                throw new ArgumentException($"Блюдо '{name}' отсутствует в меню.");
            items.Add(item);
        }

        var order = new Order(nextOrderId++, items, type, discount, tax, deliveryFee);
        orders.Add(order);
        return order;
    }

    public Order GetOrderById(int id) => orders.Find(o => o.GetId() == id);

    public List<Order> GetOrdersByStatus(OrderStatus status) =>
        orders.FindAll(o => o.GetStatus() == status);

    public OrderStatus? GetOrderStatus(int orderId) =>
        GetOrderById(orderId)?.GetStatus();

    public List<(OrderStatus Status, DateTime Timestamp)> GetOrderHistory(int orderId) =>
        GetOrderById(orderId)?.GetStatusHistory();

    public List<(int OrderId, OrderStatus Status, DateTime Timestamp)> GetAllOrdersWithHistory()
    {
        var allHistory = new List<(int OrderId, OrderStatus Status, DateTime Timestamp)>();
        foreach (var order in orders)
        {
            foreach (var history in order.GetStatusHistory())
            {
                allHistory.Add((order.GetId(), history.Status, history.Timestamp));
            }
        }
        return allHistory;
    }
}
