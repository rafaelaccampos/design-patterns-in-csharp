namespace DesignPatterns.Creational.Prototype
{
    public class Car : IVehiclePrototype
    {
        public string Model { get; private set; }

        public string Color { get; private set; }

        public int Seats { get; private set; }

        public Car(string model, string color, int seats)
        {
            Model = model;
            Color = color;
            Seats = seats;
        }

        public IVehiclePrototype Clone()
        {
            return (IVehiclePrototype)MemberwiseClone();
        }

        public void SetColor(string color)
        {
            Color = color;
        }

        public override string ToString()
        {
            return $"Car: {Model}, Color: {Color}, Seats: {Seats}";
        }
    }
}
