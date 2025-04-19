namespace PetPalsManagementSystem.entity
{
    public class Cat : Pet
    {
        public string CatColor { get; set; }

        public Cat(string name, int age, string breed,bool availableForAdoption,   string catColor)
            : base(name, age, breed,availableForAdoption ,"cat")
        {
            CatColor = catColor;
        }

        public override string ToString()
        {
            return base.ToString() + $", Cat Color: {CatColor}";
        }
    }
}
