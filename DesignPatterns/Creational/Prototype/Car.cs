namespace DesignPatterns.Creational.Prototype
{
    public class Car : IVehiclePrototype
    {
        public string Model { get; set; }

        public string Color { get; set; }

        public int Seats { get; set; }

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

        public override string ToString()
        {
            return $"Car: {Model}, Color: {Color}, Seats: {Seats}";
        }
    }
}
