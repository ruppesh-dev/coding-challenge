using System;
using System.Collections.Generic;
using PetPalsManagementSystem.Entity;
using PetPalsManagementSystem.exception;
using PetPalsManagementSystem.dao;
using PetPalsManagementSystem.entity;

namespace PetPalsManagementSystem
{
    class MainModule
    {
        
        private static IPetService petService = new PetServiceImpl();
        private static IDonationService donationService = new DonationServiceImpl();
        private static IAdoptionEventDAO adoptionEventDAO = new AdoptionEventDAOImpl();

        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Welcome to PetPals Management System");
                Console.WriteLine("1. Add Pet");
                Console.WriteLine("2. List Available Pets");
                Console.WriteLine("3. Make Donation");
                Console.WriteLine("4. Register Participant For AdoptionEvent");
                Console.WriteLine("5. Remove Pet");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option (1-6): ");

                try
                {
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            try
                            {
                                Console.WriteLine("Enter pet details:");

                                Console.Write("Name: ");
                                string name = Console.ReadLine();

                                Console.Write("Age: ");
                                int age = int.Parse(Console.ReadLine());

                                Console.Write("Breed: ");
                                string breed = Console.ReadLine();

                                Console.Write("Is the pet available for adoption? (yes/no): ");
                                string adoptionStatus = Console.ReadLine().ToLower();
                                bool availableForAdoption = adoptionStatus == "yes";

                                Console.Write("Enter the type of pet (e.g., Dog, Cat): ");
                                string type = Console.ReadLine();

                                Pet pet;

                                if (type.ToLower() == "dog")
                                {
                                    Console.Write("Enter the dog's breed: ");
                                    string dogBreed = Console.ReadLine();
                                    pet = new Dog(name, age, breed, availableForAdoption, dogBreed);
                                }
                                else if (type.ToLower() == "cat")
                                {
                                    Console.Write("Enter the cat's color: ");
                                    string catColor = Console.ReadLine();
                                    pet = new Cat(name, age, breed, availableForAdoption, catColor);
                                }
                                else
                                {
                                    pet = new Pet(name, age, breed, availableForAdoption, type);
                                }

                                petService.AddPet(pet);
                                Console.WriteLine("Pet added successfully!");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                            break;

                        case 2:
                            try
                            {
                                List<Pet> pets = petService.GetAvailablePets();

                                if (pets.Count > 0)
                                {
                                    Console.WriteLine("Available Pets:");
                                    foreach (var pet in pets)
                                    {
                                        Console.WriteLine($"Name: {pet.Name}, Age: {pet.Age}, Breed: {pet.Breed}, Type: {pet.Type}");

                                        if (pet is Dog dog)
                                        {
                                            Console.WriteLine($"Dog Breed: {dog.DogBreed}");
                                        }
                                        else if (pet is Cat cat)
                                        {
                                            Console.WriteLine($"Cat Color: {cat.CatColor}");
                                        }

                                        Console.WriteLine();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No pets available for adoption.");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                            break;

                        case 3:
                            try
                            {
                                Console.WriteLine("Enter donation details:");

                                Console.Write("Donor Name: ");
                                string donorName = Console.ReadLine();

                                Console.Write("Amount: ");
                                decimal amount = decimal.Parse(Console.ReadLine());

                                if (amount < 10)
                                {
                                    throw new InsufficientFundsException("Donation amount must be at least $10.");
                                }

                                CashDonation donation = new CashDonation(donorName, amount, DateTime.Now);
                                donationService.RecordCashDonation(donation);

                                Console.WriteLine("Donation recorded successfully!");
                            }
                            catch (InsufficientFundsException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                            break;

                        case 4:
                            try
                            {
                                List<AdoptionEvent> events = adoptionEventDAO.GetUpcomingEvents();
                                if (events.Count == 0)
                                {
                                    Console.WriteLine("No adoption events available.");
                                    break;
                                }

                                Console.WriteLine("Adoption Events:");
                                foreach (var ev in events)
                                {
                                    Console.WriteLine($"Event ID: {ev.EventID}, Name: {ev.EventName}, Date: {ev.EventDate.ToShortDateString()}, Location: {ev.Location}");
                                }

                                Console.Write("Enter Participant Name: ");
                                string participantName = Console.ReadLine();

                                Console.Write("Enter Participant Type ('Shelter', 'Adopter', 'Volunteer', 'Sponsor'): ");
                                string participantType = Console.ReadLine();

                                Console.Write("Enter Event ID to register for: ");
                                int selectedEventId = int.Parse(Console.ReadLine());

                                adoptionEventDAO.RegisterParticipant(participantName, participantType, selectedEventId);

                                Console.WriteLine("Participant registered successfully for the event!");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                            break;

                        case 5:
                            Console.Write("Enter the name of the pet to remove: ");
                            string petNameToRemove = Console.ReadLine();

                            try
                            {
                                petService.RemovePet(petNameToRemove);  
                                Console.WriteLine("Pet removed successfully.");
                            }
                            catch (PetNotFoundException ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Unexpected error: {ex.Message}");
                            }
                            break;



                        case 6:
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("Invalid choice, please try again.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }

                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
        }
    }
}
