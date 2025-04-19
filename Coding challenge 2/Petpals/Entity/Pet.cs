namespace PetPalsManagementSystem.entity
{
    public class Pet
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Breed { get; set; }
        public bool AvailableForAdoption { get; set; }
        public string Type { get; set; }  

        public Pet(string name, int age, string breed, bool availableForAdoption, string type)
        {
            Name = name;
            Age = age;
            Breed = breed;
            AvailableForAdoption = availableForAdoption;
            Type = type; 
        }

        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}, Breed: {Breed}, AvailableForAdoption: {AvailableForAdoption}, Type: {Type}";
        }
    }
}