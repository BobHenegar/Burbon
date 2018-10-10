using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.EntityFramework.Helpers
{
    public static class MigrationHelper
    {
        public static void RunMigrations(this ApplicationDbContext context, List<string> migrationsApplied)
        {
            // Place newest migrations at the bottom!
        }
        public static void SeedData(this ApplicationDbContext context)
        {
            if(context.People.Count() < 10)
            {
                for (var i = 0; i< 5000; ++i)
                {
                    var randomGen = new Random();
                    var isHomeOrWork = randomGen.Next(0, 1) == 1 ? true : false;
                    context.People.Add(new Person()
                    {
                        Customer = new Customer(),
                        PerFirstName = Faker.Name.First(),
                        PerLastName = Faker.Name.Last(),
                        PerDateOfBirth = DateTimeOffset.Now.AddYears(-1 * randomGen.Next(25, 91)),
                        PerGender = isHomeOrWork ? "Male" : "Female",
                        Addresses = new List<Address>()
                        {
                            new Address
                            {
                                AdrAddress1 = Faker.Address.StreetAddress(),
                                AdrCity = Faker.Address.City(),
                                AdrState = Faker.Address.UsState(),
                                AdrZip = Faker.Address.ZipCode(),
                                AdrIsPrimaryAddress = true,
                                AdrIsHomeAddress = isHomeOrWork,
                                AdrIsWorkAddress = !isHomeOrWork
                            }
                        },
                        Emails = new List<Email>()
                        {
                            new Email
                            {
                                EmlIsPersonalEmail = isHomeOrWork,
                                EmlIsWorkEmail = !isHomeOrWork,
                                EmlIsPrimaryEmail = true,
                                EmlAddress = Faker.Internet.Email()
                            }
                        },
                        Phones = new List<Phone>()
                        {
                            new Phone
                            {
                                PhoNumber = Faker.Phone.Number(),
                                PhoIsHome = isHomeOrWork,
                                PhoIsWork = !isHomeOrWork,
                                PhoIsPrimary = true
                            }
                        }

                    });
                }
                context.SaveChanges();
            }
        }
    }
}
