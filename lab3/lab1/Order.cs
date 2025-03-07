using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum OrderStatus { Preparing, Delivering, Completed }
public enum OrderType { Standard, Express, Personalized }


public class Order
{
    private int id;
    private List<MenuItem> items;
    private OrderType type;
    private OrderStatus status;
    private double totalPrice;
    private List<(OrderStatus Status, DateTime Timestamp)> statusHistory;

    public Order(int id, List<MenuItem> items, OrderType type, double discount = 0, double tax = 0, double deliveryFee = 0)
    {
        this.id = id;
        this.items = items;
        this.type = type;
        this.totalPrice = CalculateTotal(discount, tax, deliveryFee);
        this.status = OrderStatus.Preparing;
        this.statusHistory = new List<(OrderStatus, DateTime)> { (OrderStatus.Preparing, DateTime.Now) };
    }

    private double CalculateTotal(double discount, double tax, double deliveryFee)
    {
        double basePrice = items.Sum(item => item.Price);
        return (basePrice - discount) * (1 + tax) + deliveryFee;
    }

    public int GetId() => id;
    public List<MenuItem> GetItems() => items;
    public OrderType GetOrderType() => type;
    public double GetTotalPrice() => totalPrice;
    public OrderStatus GetStatus() => status;
    public List<(OrderStatus Status, DateTime Timestamp)> GetStatusHistory() => statusHistory;

    public void UpdateStatus(OrderStatus newStatus)
    {
        status = newStatus;
        statusHistory.Add((newStatus, DateTime.Now));
    }
}
