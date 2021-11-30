namespace Battlefield.Core.Domain.Orders;
public class MoveOrder : IOrder
{
    public Coordinates Destination { get; set; }
    public MoveOrder(Coordinates dest)
    {
        Destination = dest;
    }
}
