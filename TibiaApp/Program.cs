using TibiaModels.BL;
using TibiaRepositories.BL;

var characterService = new CharacterService();
var npcRepository = new NpcRepository();
var itemRepository = new ItemRepository();
var monsterRepository = new MonsterRepository();
var characterRepository = new CharacterRepository();
var character = new Character();

int login = 0;
while (login != 1 && login != 2)
{
    login = Convert.ToInt32(Console.ReadLine());
    if (login == 1)
    {
        Console.WriteLine("Character name: ");
        var charName = Console.ReadLine();
        Console.WriteLine("Vocation: ");
        var charVocation = Console.ReadLine();
        Console.WriteLine("Residence: ");
        var charResidence = Console.ReadLine();
        Console.WriteLine("Guild: ");
        var charGuild = Console.ReadLine();
        character = new Character()
        {
            Name = charName,
            Vocation = charVocation,
            Residence = charResidence,
            Guild = charGuild,
            Lvl = 1,
            Experience = 0,
            MaxCapacity = 500,
            CurrentCapacity = 500
        };
        if (characterService.isValid(character))
        {
            characterService.Create(character);
            Console.WriteLine("Character created");
        }
        break;
    }
    if (login == 2)
    {
        Console.WriteLine("Account number:");
        var accNumber = Convert.ToInt32(Console.ReadLine());
        if (characterRepository.IsExist(accNumber))
        {
            character = characterRepository.Get(accNumber);
            Console.WriteLine($"You have logged on {character.Name}");
        }
        else
        {
            Console.WriteLine("Wrong account number");
        }
    }
}
bool inProgress = true;

while (inProgress == true)
{
    character = characterRepository.GetWithItems(character.CharacterId);
    string task = Console.ReadLine();

    if (task == "1")
    {
        Console.WriteLine($"Name: {character.Name}");
        Console.WriteLine($"Vocation: {character.Vocation}");
        Console.WriteLine($"Level: {character.Lvl}");
        Console.WriteLine($"Guild: {character.Guild}");
        Console.WriteLine($"Residence: {character.Residence}");
        Console.WriteLine($"Max capacity: {Math.Round(character.MaxCapacity)}");
        Console.WriteLine($"Current capacity: {Math.Round(character.CurrentCapacity)}");
    }
    if (task == "2")
    {
        var monster = monsterRepository.Get(1);
        Console.WriteLine($"You killed a {monster.Name}");
    }
    if (task == "3")
    {
        var monster = monsterRepository.GetWithItems(2);
        var loot = characterService.KillMonster(character, monster);
        Console.WriteLine($"You killed a {monster.Name}");
        Console.WriteLine($"You gained {monster.Experience} exp");
        var lootCarried = characterService.GetLoot(character, loot);
        foreach (var item in lootCarried)
        {
            Console.WriteLine($"You looted a {item.Name.ToLower()}");
            loot.Remove(item);
        }
        foreach (var item in loot)
        {
            Console.WriteLine($"{item.Name} was too heavy to carry for you");
        }
    }
    if (task == "4")
    {
        var monster = monsterRepository.GetWithItems(3);
        var loot = characterService.KillMonster(character, monster);
        Console.WriteLine($"You killed a {monster.Name}");
        Console.WriteLine($"You gained {monster.Experience} exp");
        var lootCarried = characterService.GetLoot(character, loot);
        foreach (var item in lootCarried)
        {
            Console.WriteLine($"You looted a {item.Name.ToLower()}");
            loot.Remove(item);
        }
        foreach (var item in loot)
        {
            Console.WriteLine($"{item.Name} was too heavy to carry for you");
        }
    }
    if (task == "9")
    {
        Console.WriteLine("NPC name:");
        var npcName = Console.ReadLine();
        while (!npcRepository.IsExist(npcName))
        {
            Console.WriteLine($"There is no such NPC as {npcName}");
            npcName = Console.ReadLine();
        }
        Console.WriteLine("Sell item:");
        var itemName = Console.ReadLine();
        while (!itemRepository.IsExist(itemName))
        {
            Console.WriteLine($"Such item as {itemName} does not exist");
            itemName = Console.ReadLine();
        }
        var npc = npcRepository.GetByName(npcName);
        var item = itemRepository.GetByName(itemName);
        if (characterService.IsNpcBuying(npc.NpcId, item.ItemId))
        {
            if (characterService.IsInBp(character, item.ItemId))
            {
                character = characterRepository.GetWithItems(character.CharacterId);
                int price = characterService.SellItem(npc.NpcId, item.ItemId, character);
                Console.WriteLine($"You sold a {item.Name.ToLower()} for {price} gold coins");
            }
            else
            {
                Console.WriteLine($"You don't have a {item.Name}");
            }
        }
        else
        {
            Console.WriteLine($"{npc.Name} doesn't buy {item.Name}s");
        }
    }
    if (task == "0")
    {
        Console.WriteLine("Items in backpack:");
        var items = character.Equipment.ItemInstances.Where(ii => ii.ContainerId == character.Equipment.BackpackInstanceId).ToList();
        foreach (var item in items)
        {
            if (item.Quantity > 0)
            {
                Console.WriteLine($"{item.Quantity} {item.Item.Name.ToLower()}s");
            }
            else
            {
                Console.WriteLine(item.Item.Name);
            }
        }
        Console.WriteLine();
        Console.WriteLine($"Capacity: {Math.Round(character.CurrentCapacity)}");
    }
    if (task == "2137")
    {
        inProgress = false;
    }
}