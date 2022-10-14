using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TibiaAPI.Controllers;
using TibiaModels.BL;
using TibiaRepositories.BL.Interfaces;

namespace TibiaTests
{
    public class ActionsControllerTests : IDisposable
    {
        public ActionsControllerTests()
        {
            characterService = new Mock<ICharacterService>();
            characterRepository = new Mock<ICharacterRepository>();
            monsterRepository = new Mock<IMonsterRepository>();
            itemRepository = new Mock<IItemRepository>();
            npcRepository = new Mock<INpcRepository>();
            mapper = new Mock<IMapper>();
            actionsController = new ActionsController(characterService.Object, characterRepository.Object, monsterRepository.Object,
                itemRepository.Object, npcRepository.Object, mapper.Object);
        }
        private readonly Mock<ICharacterService> characterService;
        private readonly Mock<ICharacterRepository> characterRepository;
        private readonly Mock<IMonsterRepository> monsterRepository;
        private readonly Mock<IItemRepository> itemRepository;
        private readonly Mock<INpcRepository> npcRepository;
        private readonly Mock<IMapper> mapper;
        private readonly ActionsController actionsController;
        
        public void Dispose()
        {

        }
        [Fact]
        public async Task KillMonster_ReturnsOkObject()
        {
            //Arrange
            characterRepository.Setup(cr => cr.GetWithItemsAsync(It.IsAny<int>())).ReturnsAsync(new Character());
            monsterRepository.Setup(mr => mr.GetWithItemsAsync(It.IsAny<int>())).ReturnsAsync(new Monster());
            characterService.Setup(cs => cs.KillMonsterAsync(It.IsAny<Character>(), It.IsAny<Monster>())).ReturnsAsync(new List<Item>());

            //Act
            var expected = await actionsController.KillMonster(2, 3);

            //Assert
            Assert.IsType<OkObjectResult>(expected.Result);
        }
        [Fact]
        public async Task SellItem_CharacterDoesntHaveItemInBp_ReturnsBadRequest()
        {
            //Arrange
            characterRepository.Setup(cr => cr.GetWithItemsAsync(It.IsAny<int>())).ReturnsAsync(new Character());
            itemRepository.Setup(ir => ir.GetAsync(It.IsAny<int>())).ReturnsAsync(new Item());
            characterService.Setup(cs => cs.IsInBp(It.IsAny<Character>(), It.IsAny<Item>())).Returns(false);
            characterService.Setup(cs => cs.IsNpcBuying(It.IsAny<Npc>(), It.IsAny<Item>())).Returns(true);

            //Act
            var expected = await actionsController.SellItem(1, 2, 3);

            //Assert
            Assert.IsType<BadRequestResult>(expected.Result);
        }
        [Fact]
        public async Task SellItem_NpcDoesntBuyItem_ReturnsBadRequest()
        {
            //Arrange
            characterRepository.Setup(cr => cr.GetWithItemsAsync(It.IsAny<int>())).ReturnsAsync(new Character());
            itemRepository.Setup(ir => ir.GetAsync(It.IsAny<int>())).ReturnsAsync(new Item());
            characterService.Setup(cs => cs.IsInBp(It.IsAny<Character>(), It.IsAny<Item>())).Returns(true);
            characterService.Setup(cs => cs.IsNpcBuying(It.IsAny<Npc>(), It.IsAny<Item>())).Returns(false);

            //Act
            var expected = await actionsController.SellItem(1, 2, 3);

            //Assert
            Assert.IsType<BadRequestResult>(expected.Result);
        }
        [Fact]
        public async Task SellItem_EverythingMatches_ReturnsNoContent()
        {
            //Arrange
            characterRepository.Setup(cr => cr.GetWithItemsAsync(It.IsAny<int>())).ReturnsAsync(new Character());
            itemRepository.Setup(ir => ir.GetAsync(It.IsAny<int>())).ReturnsAsync(new Item());
            characterService.Setup(cs => cs.IsInBp(It.IsAny<Character>(), It.IsAny<Item>())).Returns(true);
            characterService.Setup(cs => cs.IsNpcBuying(It.IsAny<Npc>(), It.IsAny<Item>())).Returns(true);

            //Act
            var expected = await actionsController.SellItem(1, 2, 3);

            //Assert
            Assert.IsType<NoContentResult>(expected.Result);
        }
    }
}