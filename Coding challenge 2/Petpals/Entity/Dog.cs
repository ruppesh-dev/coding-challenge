namespace PetPalsManagementSystem.entity
{
    public class Dog : Pet
    {
        public string DogBreed { get; set; }

        public Dog(string name, int age, string breed, bool availableForAdoption,string dogBreed)
            : base(name, age, breed, availableForAdoption,"Dog")
        {
            DogBreed = dogBreed;
        }

        public override string ToString()
        {
            return base.ToString() + $", Dog Breed: {DogBreed}";
        }
    }
}
