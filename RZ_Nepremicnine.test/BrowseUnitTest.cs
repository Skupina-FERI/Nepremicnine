using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using RZ_nepremicnine.Data;
using RZ_nepremicnine.Models;
using RZ_nepremicnine.Pages.Properties;

namespace RZ_Nepremicine.test
{
    [TestClass]
    public class UnitTestBrowse
    {
        [TestMethod]
        //no filters test
        public async Task OnGetAsyncTestSuccess()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>() // instructions for "use fake database, not real one"
                .UseInMemoryDatabase(databaseName:"TestDatabaseSuccess")
                .Options;
            var context = new AppDbContext(options); //your DbContext following those instructions
            context.Nepremicnine.Add(new Nepremicnina {Id = 1, Regija = "Pomurska", TipNepremicnine ="Hisa", Naziv = "Testna hiska"});
            await context.SaveChangesAsync();

            var logger = NullLogger<BrowseModel>.Instance;
            var browseModel = new BrowseModel(logger, context);
            await browseModel.OnGetAsync();

            var result = browseModel.Nepremicnine[0];
            Assert.AreEqual("Pomurska", result.Regija);
            Assert.AreEqual("Hisa", result.TipNepremicnine);
            Assert.AreEqual("Testna hiska", result.Naziv);
            Assert.AreEqual(1, result.Id);
        }
        [TestMethod]
        //region filter test
        public async Task OnGetTestRegionFilter()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabaseRegion")
                .Options;
            var context = new AppDbContext(options);
            context.Nepremicnine.Add(new Nepremicnina{Id =1, Regija= "Podravska", TipNepremicnine= "Hisa", Naziv = "testna hiska", Cena = 100000});
            context.Nepremicnine.Add(new Nepremicnina{Id =2, Regija= "Pomurska", TipNepremicnine= "Stanovanje", Naziv = "drugo stanovanje", Cena = 80000});
            await context.SaveChangesAsync();

            var logger = NullLogger<BrowseModel>.Instance;
            var browseModel = new BrowseModel(logger, context);
            browseModel.SelectedRegion = "Podravska";
            await browseModel.OnGetAsync();

            Assert.AreEqual(1, browseModel.Nepremicnine.Count);
            var result = browseModel.Nepremicnine[0];
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Podravska", result.Regija);
            Assert.AreEqual("Hisa", result.TipNepremicnine);
            Assert.AreEqual("testna hiska", result.Naziv);
        }

        [TestMethod]
        //posredovanje filter test
        public async Task OnGetTestPosredovanjeFilter()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabasePosredovanje")
                .Options;
            var context = new AppDbContext(options);

            var posredovanje1 = new Posredovanje { Id = 1, Name = "Prodaja" };
            var posredovanje2 = new Posredovanje { Id = 2, Name = "Oddaja" };
            context.Posredovanja.Add(posredovanje1);
            context.Posredovanja.Add(posredovanje2);

            context.Nepremicnine.Add(new Nepremicnina
            {
                Id = 1,
                Regija = "Pomurska",
                TipNepremicnine = "Hisa",
                Naziv = "Hisa za prodajo",
                Cena = 150000,
                PosredovanjeFK = 1,
                PosredovanjeNavigation = posredovanje1
            });
            context.Nepremicnine.Add(new Nepremicnina
            {
                Id = 2,
                Regija = "Podravska",
                TipNepremicnine = "Stanovanje",
                Naziv = "Stanovanje za oddajo",
                Cena = 500,
                PosredovanjeFK = 2,
                PosredovanjeNavigation = posredovanje2
            });
            await context.SaveChangesAsync();

            var logger = NullLogger<BrowseModel>.Instance;
            var browseModel = new BrowseModel(logger, context);
            browseModel.SelectedPosredovanje = "Prodaja";
            await browseModel.OnGetAsync();

            Assert.AreEqual(1, browseModel.Nepremicnine.Count);
            var result = browseModel.Nepremicnine[0];
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Hisa za prodajo", result.Naziv);
            Assert.AreEqual("Prodaja", result.PosredovanjeNavigation.Name);
        }

        [TestMethod]
        //vrsta nepremicnine filter test
        public async Task OnGetTestVrstaNepremicnineFilter()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabaseVrsta")
                .Options;
            var context = new AppDbContext(options);

            context.Nepremicnine.Add(new Nepremicnina
            {
                Id = 1,
                Regija = "Pomurska",
                TipNepremicnine = "Stanovanje",
                Naziv = "Lepo stanovanje",
                Cena = 95000
            });
            context.Nepremicnine.Add(new Nepremicnina
            {
                Id = 2,
                Regija = "Pomurska",
                TipNepremicnine = "Hiša",
                Naziv = "Velika hisa",
                Cena = 200000
            });
            context.Nepremicnine.Add(new Nepremicnina
            {
                Id = 3,
                Regija = "Podravska",
                TipNepremicnine = "Stanovanje",
                Naziv = "Drugo stanovanje",
                Cena = 110000
            });
            await context.SaveChangesAsync();

            var logger = NullLogger<BrowseModel>.Instance;
            var browseModel = new BrowseModel(logger, context);
            browseModel.SelectedVrstaNepremicnine = "Stanovanje";
            await browseModel.OnGetAsync();

            Assert.AreEqual(2, browseModel.Nepremicnine.Count);
            Assert.IsTrue(browseModel.Nepremicnine.All(n => n.TipNepremicnine == "Stanovanje"));
        }

        [TestMethod]
        //multiple filters combined test
        public async Task OnGetTestMultipleFilters()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabaseMultiple")
                .Options;
            var context = new AppDbContext(options);

            var posredovanje = new Posredovanje { Id = 1, Name = "Prodaja" };
            context.Posredovanja.Add(posredovanje);

            context.Nepremicnine.Add(new Nepremicnina
            {
                Id = 1,
                Regija = "Pomurska",
                TipNepremicnine = "Stanovanje",
                Naziv = "Target property",
                Cena = 100000,
                PosredovanjeFK = 1,
                PosredovanjeNavigation = posredovanje
            });
            context.Nepremicnine.Add(new Nepremicnina
            {
                Id = 2,
                Regija = "Podravska",
                TipNepremicnine = "Stanovanje",
                Naziv = "Wrong region",
                Cena = 100000,
                PosredovanjeFK = 1,
                PosredovanjeNavigation = posredovanje
            });
            context.Nepremicnine.Add(new Nepremicnina
            {
                Id = 3,
                Regija = "Pomurska",
                TipNepremicnine = "Hiša",
                Naziv = "Wrong type",
                Cena = 150000,
                PosredovanjeFK = 1,
                PosredovanjeNavigation = posredovanje
            });
            await context.SaveChangesAsync();

            var logger = NullLogger<BrowseModel>.Instance;
            var browseModel = new BrowseModel(logger, context);
            browseModel.SelectedRegion = "Pomurska";
            browseModel.SelectedVrstaNepremicnine = "Stanovanje";
            browseModel.SelectedPosredovanje = "Prodaja";
            await browseModel.OnGetAsync();

            Assert.AreEqual(1, browseModel.Nepremicnine.Count);
            var result = browseModel.Nepremicnine[0];
            Assert.AreEqual("Target property", result.Naziv);
            Assert.AreEqual("Pomurska", result.Regija);
            Assert.AreEqual("Stanovanje", result.TipNepremicnine);
        }

        [TestMethod]
        //empty result test
        public async Task OnGetTestNoResults()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabaseEmpty")
                .Options;
            var context = new AppDbContext(options);

            context.Nepremicnine.Add(new Nepremicnina
            {
                Id = 1,
                Regija = "Pomurska",
                TipNepremicnine = "Hisa",
                Naziv = "Test hisa",
                Cena = 100000
            });
            await context.SaveChangesAsync();

            var logger = NullLogger<BrowseModel>.Instance;
            var browseModel = new BrowseModel(logger, context);
            browseModel.SelectedRegion = "Koroška";
            await browseModel.OnGetAsync();

            Assert.AreEqual(0, browseModel.Nepremicnine.Count);
        }

        [TestMethod]
        //null/empty filter test
        public async Task OnGetTestNullFilters()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabaseNull")
                .Options;
            var context = new AppDbContext(options);

            context.Nepremicnine.Add(new Nepremicnina
            {
                Id = 1,
                Regija = "Pomurska",
                TipNepremicnine = "Hisa",
                Naziv = "Prva hisa",
                Cena = 100000
            });
            context.Nepremicnine.Add(new Nepremicnina
            {
                Id = 2,
                Regija = "Podravska",
                TipNepremicnine = "Stanovanje",
                Naziv = "Stanovanje",
                Cena = 80000
            });
            await context.SaveChangesAsync();

            var logger = NullLogger<BrowseModel>.Instance;
            var browseModel = new BrowseModel(logger, context);
            browseModel.SelectedRegion = null;
            browseModel.SelectedPosredovanje = null;
            browseModel.SelectedVrstaNepremicnine = "";
            await browseModel.OnGetAsync();

            Assert.AreEqual(2, browseModel.Nepremicnine.Count);
        }

        [TestMethod]
        //large dataset test
        public async Task OnGetTestLargeDataset()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabaseLarge")
                .Options;
            var context = new AppDbContext(options);

            for (int i = 1; i <= 50; i++)
            {
                context.Nepremicnine.Add(new Nepremicnina
                {
                    Id = i,
                    Regija = i % 2 == 0 ? "Pomurska" : "Podravska",
                    TipNepremicnine = i % 3 == 0 ? "Stanovanje" : "Hiša",
                    Naziv = $"Property {i}",
                    Cena = 50000 + (i * 1000)
                });
            }
            await context.SaveChangesAsync();

            var logger = NullLogger<BrowseModel>.Instance;
            var browseModel = new BrowseModel(logger, context);
            browseModel.SelectedRegion = "Pomurska";
            await browseModel.OnGetAsync();

            Assert.AreEqual(25, browseModel.Nepremicnine.Count);
            Assert.IsTrue(browseModel.Nepremicnine.All(n => n.Regija == "Pomurska"));
        }

        [TestMethod]
        //posredovanje null navigation test
        public async Task OnGetTestPosredovanjeNullNavigation()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabasePosNull")
                .Options;
            var context = new AppDbContext(options);

            var posredovanje = new Posredovanje { Id = 1, Name = "Prodaja" };
            context.Posredovanja.Add(posredovanje);

            context.Nepremicnine.Add(new Nepremicnina
            {
                Id = 1,
                Regija = "Pomurska",
                TipNepremicnine = "Hisa",
                Naziv = "With posredovanje",
                Cena = 100000,
                PosredovanjeFK = 1,
                PosredovanjeNavigation = posredovanje
            });
            context.Nepremicnine.Add(new Nepremicnina
            {
                Id = 2,
                Regija = "Pomurska",
                TipNepremicnine = "Hisa",
                Naziv = "Without posredovanje",
                Cena = 120000,
                PosredovanjeNavigation = null
            });
            await context.SaveChangesAsync();

            var logger = NullLogger<BrowseModel>.Instance;
            var browseModel = new BrowseModel(logger, context);
            browseModel.SelectedPosredovanje = "Prodaja";
            await browseModel.OnGetAsync();

            Assert.AreEqual(1, browseModel.Nepremicnine.Count);
            Assert.AreEqual("With posredovanje", browseModel.Nepremicnine[0].Naziv);

        }
    }
}
