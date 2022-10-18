using DB1;
using Moq;
using TibiaModels.BL;
using TibiaRepositories.BL;
using TibiaRepositories.BL.Interfaces;

namespace TibiaTests
{
    public class CharacterServiceTests : IDisposable
    {
        public CharacterServiceTests()
        {
            characterRepository = new Mock<ICharacterRepository>();
            itemRepository = new Mock<IItemRepository>();
            itemInstanceRepository = new Mock<IItemInstanceRepository>();
            characterService = new CharacterService(characterRepository.Object, itemRepository.Object, itemInstanceRepository.Object);
        }
        private readonly Mock<ICharacterRepository> characterRepository;
        private readonly Mock<IItemRepository> itemRepository;
        private readonly Mock<IItemInstanceRepository> itemInstanceRepository;
        private readonly CharacterService characterService;
        public void Dispose()
        {

        }

        [Fact]
        public async Task CreateCharacterAsync_IsMaxAndCurrentCap500()
        {
            //Arrange
            characterRepository.Setup(cr => cr.SaveChangesAsync()).ReturnsAsync(true);
            characterRepository.Setup(cr => cr.AddAsync(It.IsAny<Character>())).ReturnsAsync(true);
            itemRepository.Setup(ir => ir.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(new Item());
            var character = new Character()
            {
                Name = "Name",
                Vocation = "Vocation",
                Residence = "Residence",
                Guild = "Guild"
            };

            //Act
            var characterCreated = await characterService.CreateAsync(character);

            //Assert
            Assert.Equal(500, characterCreated.MaxCapacity);
            Assert.Equal(500, characterCreated.CurrentCapacity);
        }
        [Fact]
        public async Task KillMonsterAsync_IsAddingExp()
        {
            //Arrange
            var character = new Character()
            {
                Experience = 500
            };
            var monster = new Monster()
            {
                Experience = 245,
                ItemMonsters = new Mock<List<ItemMonster>>().Object
            };

            //Act
            await characterService.KillMonsterAsync(character, monster);

            //Assert
            Assert.Equal(745, character.Experience);
        }
        [Fact]
        public async Task GetLootAsync_LootBeingAddedToCharacter()
        {
            //Arrange
            var loot = new List<Item>()
            {
                new Item()
                {
                    Name = "Jacula's Katana",
                    ItemId = 4
                }
            };
            var character = new Character()
            {
                Equipment = new Equipment()
                {
                    EquipmentId = 2137,
                    BackpackInstanceId = 40
                },
                CurrentCapacity = 1500,
            };
            ItemInstance result = null;
            itemInstanceRepository.Setup(iir => iir.AddAsync(It.IsAny<ItemInstance>())).Callback<ItemInstance>(r => result = r);

            //Act
            await characterService.GetLootAsync(character, loot);


            Assert.Equal(4, result.ItemId);
            Assert.Equal(2137, result.EquipmentId);
            Assert.Equal(40, result.ContainerId);
        }
        [Fact]
        public async Task GetLootAsync_LootTooHeavyNotBeingCollected()
        {
            //Arrange
            var loot = new List<Item>()
            {
                new Item()
                {
                    Name = "Jacula's Katana",
                    ItemId = 4,
                    Weight = 60
                }
            };
            var character = new Character()
            {
                Equipment = new Equipment()
                {
                    EquipmentId = 2137,
                    BackpackInstanceId = 40
                },
                CurrentCapacity = 30,
            };
            ItemInstance result = null;
            itemInstanceRepository.Setup(iir => iir.AddAsync(It.IsAny<ItemInstance>())).Callback<ItemInstance>(r => result = r);

            //Act
            await characterService.GetLootAsync(character, loot);

            //Arrange
            Assert.Equal(null, result);
        }
        [Fact]
        public async Task SellItemAsync_SellingItemIncreasesCharacterCapacity()
        {
            //Arrange
            var character = new Character()
            {
                Equipment = new Equipment()
                {
                    BackpackInstanceId = 3,
                    EquipmentId = 5,
                    ItemInstances = new List<ItemInstance>()
                },
                CurrentCapacity = 300,
                MaxCapacity = 1000
            };
            var itemSold = new Item()
            {
                Weight = 56,
                Quantity = 1,
            };
            var npc = new Npc()
            {
                ItemNpcs = new List<ItemNpc>()
                {
                    new ItemNpc()
                    {
                        Price = 0
                    }
                }
            };
            itemRepository.Setup(ir => ir.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(new Item());
            itemRepository.Setup(ir => ir.GetAsync(It.IsAny<int>())).ReturnsAsync(new Item());

            //Act
            await characterService.SellItemAsync(npc, itemSold, character);

            //Assert
            Assert.Equal(356, character.CurrentCapacity);
        }
        [Fact]
        public void IsInBp_BpContainsSelectedItem()
        {
            //Arrange
            var character = new Character()
            {
                Equipment = new Equipment()
                {
                    BackpackInstanceId = 7,
                    ItemInstances = new List<ItemInstance>()
                    {
                        new ItemInstance()
                        {
                            ItemId = 3,
                            ContainerId = 7
                        }
                    }
                }
            };
            var item = new Item()
            {
                ItemId = 3
            };

            //Act
            var expected = characterService.IsInBp(character, item);

            //Assert
            Assert.True(expected);
        }
        [Fact]
        public void IsNpcBuying_NpcBuysSelectedItem()
        {
            //Arrange
            var npc = new Npc()
            {
                ItemNpcs = new List<ItemNpc>()
                {
                    new ItemNpc()
                    {
                        ItemId = 4
                    }
                }
            };
            var item = new Item()
            {
                ItemId = 4
            };

            //Act
            var expected = characterService.IsNpcBuying(npc, item);

            //Assert
            Assert.True(expected);
        }
        [Fact]
        public void GetCharactersItemsInBp_ItemsAreShowedOnlyWithContainerIdMatchingBackpackInstanceId()
        {
            //Arrange
            var character = new Character()
            {
                Equipment = new Equipment()
                {
                    BackpackInstanceId = 54,
                    ItemInstances = new List<ItemInstance>()
                    {
                        new ItemInstance()
                        {
                            ContainerId = 54,
                            Item = new Item()
                            {
                                ItemId = 4,
                                Name = "Name",
                                Weight = 30,
                                Quantity = 1
                            }
                        },
                        new ItemInstance()
                        {
                            ContainerId = 54,
                            Item = new Item()
                            {
                                ItemId = 6,
                                Name = "Name",
                                Weight = 30,
                                Quantity = 1
                            }
                        },
                        new ItemInstance()
                        {
                            ContainerId = 23,
                            Item = new Item()
                            {
                                ItemId = 1,
                                Name = "Name",
                                Weight = 30,
                                Quantity = 1
                            }
                        }
                    }
                }
            };

            //Act
            var itemsInBp = characterService.GetCharacterItemsInBp(character);

            //Assert
            var expected = new List<Item>()
            {
                new Item()
                {
                    ItemId = 4,
                    Name = "Name",
                    Weight = 30,
                    Quantity = 1
                },
                new Item()
                {
                    ItemId = 6,
                    Name = "Name",
                    Weight = 30,
                    Quantity = 1
                }
            };
            Assert.Equal(expected.Select(e => e.ItemId), itemsInBp.Select(iib => iib.ItemId));
        }
        [Fact]
        public void IsValid_CharacterIsProper()
        {
            //Arrange
            var character = new Character()
            {
                Name = "Name",
                Vocation = "Vocation",
                Residence = "Residence"
            };

            //Act
            var expected = characterService.IsValid(character);

            //Assert
            Assert.True(expected);
        }
        [Fact]
        public void IsValid_CharacterIsNotProper()
        {
            //Arrange
            var character = new Character()
            {
                Name = "Name",
                Vocation = "Vocation"
            };

            //Act
            var expected = characterService.IsValid(character);

            //Assert
            Assert.False(expected);
        }
        [Fact]
        public void SetLevel_LevelCalculatedOk()
        {
            //Arrange
            var character = new Character()
            {
                Experience = 800,
                Lvl = 2
            };

            //Act
            var expected = characterService.SetLevel(character);

            //Assert
            Assert.Equal(5, expected.Lvl);
        }
    }
}