namespace Lab4_Varych
{
    // Інтерфейс, що задає обов'язкову поведінку для будь-якого виробу
    public interface IProduct
    {
        string Name { get; set; }
        double GetWeight();
        double GetPrice();
        void DisplayInfo();
    }
}
