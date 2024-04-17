using LAB05.Classes;
using System.Linq;

namespace LAB05.Services
{
    public interface IMockDb
    {
        public ICollection<Animal> GetAllAnimals();
        public Animal? GetAnimalById(Int32 id);

        public Animal? AddAnimal(Animal animal);
        public Animal? DeleteAnimalById(int id);
        public Animal? UpdateAnimalById(int id, Animal updatedAnimal);

        public ICollection<Visit> GetAllVisitsByAnimalId(Int32 animalId);
        public Visit? AddVisit(Visit visit);
    }

    public class MockDb : IMockDb
    {
        public static ICollection<Animal>? _animals;
        public static ICollection<Visit>? _visits;

        public MockDb()
        {
            _animals = new List<Animal>
            {
                new Animal
                {
                    Id = 1,
                    Name = "Chmurka",
                    Category = "Pies",
                    Mass = 40.0f,
                    FurColor = 0x00947867
                },
                new Animal
                {
                    Id = 2,
                    Name = "Rupert",
                    Category = "Kot",
                    Mass = 10.0f,
                    FurColor = 0x00A29A98
                }
            };

            _visits = new List<Visit>
            {
                new Visit
                {
                    Date = new DateTime(2024, 5, 14, 15, 30, 0),
                    Animal = _animals.ElementAt(0),
                    Descripton = "Strzyżenie",
                    Cost = 100.0f
                },
                new Visit
                {
                    Date = new DateTime(2024, 11, 20, 14, 0, 0),
                    Animal = _animals.ElementAt(0),
                    Descripton = "Obcinanie pazurów",
                    Cost = 70.0f   
                },
                new Visit
                {
                    Date = new DateTime(2024, 7, 13, 11, 30, 0),
                    Animal = _animals.ElementAt(0),
                    Descripton = "Strzyżenie",
                    Cost = 150.0f
                }
            };
        }

        public Visit? AddVisit(Visit visit)
        {
            if(visit.Animal != null)
            {
                if (!_animals.Contains(visit.Animal)) _animals.Add(visit.Animal);
                _visits.Add(visit);
            }
            return visit;
        }

        public ICollection<Visit> GetAllVisitsByAnimalId(Int32 animalId)
        {
            return _visits.Where((visit) => ((visit.Animal == null ? false : visit.Animal.Id == animalId))).ToList();
        }




        public Animal? AddAnimal(Animal animal)
        {
            _animals.Add(animal);
            return animal;
        }

        public Animal? DeleteAnimalById(Int32 id)
        {
            Animal? animalFromList = GetAnimalById(id);
            if(animalFromList != null)
            {
                _animals.Remove(animalFromList);
            }
            return animalFromList;
        }

        public ICollection<Animal> GetAllAnimals()
        {
            return _animals;
        }

        public Animal? GetAnimalById(Int32 id)
        {
            return _animals.FirstOrDefault((animal) => (animal.Id == id));
        }

        public Animal? UpdateAnimalById(Int32 id, Animal updatedAnimal)
        {
            Animal? animalFromList = GetAnimalById(id);
            
            if(animalFromList != null)
            {
                animalFromList.Id = updatedAnimal.Id;
                animalFromList.Name = updatedAnimal.Name;
                animalFromList.Mass = updatedAnimal.Mass;
                animalFromList.FurColor = updatedAnimal.FurColor;
            }

            return animalFromList;
        }
    }
}
