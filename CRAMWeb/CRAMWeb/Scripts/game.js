// Game Time
// ----------------------------------------------------------------------------

// Minute and second
var gameMinute = 0;
var gameSecond = 0;

// Commands to be run on game tick (one second)
function gameTick() {
    gameSecond++;
    if (gameSecond >= 60) {
        gameMinute++;
        gameSecond = 0;
    }
    document.getElementById("timer").innerHTML = pad(gameMinute, 2) + ":" + pad(gameSecond, 2);

    produceResources();
    updateResourceStash();
    updateSelectedDistrict();

    if (! districts.Castle.canUpgradeLevel()) {
        alert("Congratulations! You won the game! Refresh the page to start again");
        clearInterval(gameTimer);
    }
    syncGameState();
}

// Repeat timer function every second to update it
var gameTimer = setInterval(gameTick, 1000);

// Pad numbers to size with zeros (used for game time)
function pad(num, size) {
    var s = num + "";
    while (s.length < size) s = "0" + s;
    return s;
}

// Classes
// ----------------------------------------------------------------------------

class GameState {
    constructor(id, gameState = null) {
        // Game State Id
        this.Id = id;
        if (gameState != null) {
            this.Food = gameState.Food;
            this.Wood = gameState.Wood;
            this.Stone = gameState.Stone;
            this.Gold = gameState.Gold;
            this.Soldiers = gameState.Soldiers;
            this.FarmsLevel = gameState.FarmsLevel;
            this.LumberjackLevel = gameState.LumberjackLevel;
            this.CastleLevel = gameState.CastleLevel;
            this.HousingLevel = gameState.HousingLevel;
            this.MinesLevel = gameState.MinesLevel;
        }
        else {
            this.Food = 0;
            this.Wood = 0;
            this.Stone = 0;
            this.Gold = 0;
            this.Soldiers = 0;
            this.FarmsLevel = 1;
            this.LumberjackLevel = 1;
            this.CastleLevel = 1;
            this.HousingLevel = 1;
            this.MinesLevel = 1;
        }
    }

    // Load From Variables
    load(stash, districts) {
        this.Food = stash.food;
        this.Wood = stash.wood;
        this.Stone = stash.stone;
        this.Gold = stash.gold;

        for (var key in districts) {
            var district = districts[key];
            switch (district.name) {
                case "Farm":
                    this.FarmsLevel = district.level;
                    break;
                case "Lumberjack":
                    this.LumberjackLevel = district.level;
                    break;
                case "Mines":
                    this.MinesLevel = district.level;
                    break;
                case "Housing":
                    this.HousingLevel = district.level;
                    break;
                case "Castle":
                    this.CastleLevel = district.level;
                    break;
            }
        }
    }

    // Save To Variables
    save(stash, districts) {
        stash.food = this.Food;
        stash.wood = this.Wood;
        stash.stone = this.Stone;
        stash.gold = this.Gold;

        for (var key in districts) {
            var district = districts[key];
            switch (district.name) {
                case "Farm":
                    district.level = this.FarmsLevel;
                    break;
                case "Lumberjack":
                    district.level = this.LumberjackLevel;
                    break;
                case "Mines":
                    district.level = this.MinesLevel;
                    break;
                case "Housing":
                    district.level = this.HousingLevel;
                    break;
                case "Castle":
                    district.level = this.CastleLevel;
                    break;
            }
        }
    }
}

// District class
class District {
    constructor(name, level, size, description, resourceCost, resourceProduce) {
        // District name
        this.name = name;
        // District size
        this.level = level;
        // District image size
        this.size = size;
        // District description
        this.description = description;
        // District Upgrade resource cost
        this.resourceCost = resourceCost;
        // District resources produced
        this.resourceProduce = resourceProduce;
        this.updateImage();
    }

    // Update image for district
    updateImage() {
        // Create image with size
        this.image = new Image(this.size.width, this.size.height);

        // Draw image onto context
        this.image.onload = () => {
            ctx.drawImage(this.image, this.size.x, this.size.y, this.size.width, this.size.height);
        }

        // Set source of image
        this.image.src = this.imageSrc();
    }

    // District image source
    imageSrc() {
        return `/Content/Images/${this.name}${this.level+1}.png`;
    }

    // District thumbnail source
    thumbnailSrc() {
        return `/Content/Images/${this.name}Thumbnail${this.level+1}.png`;
    }

    // Resource Cost by level
    levelResourceCost() {
        return this.resourceCost.increase(this.level + 1);
    }

    // Resource Produced by level
    levelResourceProduce() {
        return this.resourceProduce.increase(this.level + 1);
    }

    // Can upgrade district?
    canUpgradeLevel() {
        return this.level < 3;
    }
}

// Point class
class Point {
    constructor(x, y) {
        this.x = x;
        this.y = y;
    }
}

// Size class
class Size {
    constructor(width, height, x = 0, y = 0) {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
    }
}

// Resources Class: Resource Stash or Resource Cost
class Resources {
    constructor(food = 0, wood = 0, stone = 0, gold = 0) {
        this.food = food;
        this.wood = wood;
        this.stone = stone;
        this.gold = gold;
    }

    // Is one resource stash sufficient to pay resource cost?
    sufficient(resourceCost) {
        return this.food >= resourceCost.food &&
            this.wood >= resourceCost.wood &&
            this.stone >= resourceCost.stone &&
            this.gold >= resourceCost.gold;
    }

    // Subtract one resources from another
    subtract(resourceCost) {
        this.food -= resourceCost.food;
        this.wood -= resourceCost.wood;
        this.stone -= resourceCost.stone;
        this.gold -= resourceCost.gold;
    }
    
    // Add one resources from another
    add(resourceCost) {
        this.food += resourceCost.food;
        this.wood += resourceCost.wood;
        this.stone += resourceCost.stone;
        this.gold += resourceCost.gold;
    }

    // Multiply all resource by amount
    increase(amount) {
        return new Resources(this.food * amount, this.wood * amount, this.stone * amount, this.gold * amount);
    }
}

// Game Data
// ----------------------------------------------------------------------------
var gameId = 0;

// Setup Districts
// ----------------------------------------------------------------------------

var districtNames = ["Farm", "Lumberjack", "Mines", "Housing", "Castle"];
var districts = {};
var districtSizes = {
    Farm: new Size(220, 275),
    Housing: new Size(220, 275),
    Castle: new Size(120, 620),
    Lumberjack: new Size(220, 345),
    Mines: new Size(220, 345)
};
districtSizes.Lumberjack.y = districtSizes.Farm.height;
districtSizes.Castle.x = districtSizes.Farm.width;
districtSizes.Mines.x = districtSizes.Farm.width + districtSizes.Castle.width;
districtSizes.Mines.y = districtSizes.Farm.height;
districtSizes.Housing.x = districtSizes.Farm.width + districtSizes.Castle.width;

// Currently selected district
var selectedDistrict = "Farm";
// Current resource stash
var resourceStash = new Resources();

// Create districts && Initialize images
for (var districtName of districtNames) {
    // Select size
    var size = districtSizes[districtName];

    var description = "";
    var resourceCost = new Resources();
    var resourceProduce = new Resources();
    switch (districtName) {
        case "Farm":
            description = "Upgrade the Farms to increase food production.";
            resourceCost = new Resources(130);
            resourceProduce = new Resources(5);
            break;
        case "Lumberjack":
            description = "Upgrade the Lumberjack to increase wood production.";
            resourceCost = new Resources(0, 130);
            resourceProduce = new Resources(0, 5);
            break;
        case "Mines":
            description = "Upgrade the Mines to increase gold and stone production.";
            resourceCost = new Resources(0, 130, 30, 30);
            resourceProduce = new Resources(0, 0, 3, 3);
            break;
        case "Housing":
            description = "Upgrade Housing to increase maximum population size.";
            resourceCost = new Resources(0, 50);
            break;
        case "Castle":
            description = "Complete the Castle to protect the village and complete the game!";
            resourceCost = new Resources(400, 300, 250, 250);
            break;
    }
    districts[districtName] = new District(districtName, 0, size, description, resourceCost, resourceProduce);
}

// Produce Resources for all districts
function produceResources() {
    for (var key in districts) {
        var district = districts[key];
        resourceStash.add(district.levelResourceProduce());
    }
}

// Setup Map
// ----------------------------------------------------------------------------

// Map canvas
var canvas = document.getElementById('map');
var ctx = canvas.getContext('2d');
canvas.width = districtSizes.Farm.width * 2 + districtSizes.Castle.width;
canvas.height = districtSizes.Castle.height;

// Update District Info location and draw districts on map
// ----------------------------------------------------------------------------
// Set districtInfo panel to right of map
var districtInfoDiv = document.getElementById('districtInfo');
districtInfoDiv.style.marginLeft = `${canvas.width + 10}px`;

// Event Handlers
// ----------------------------------------------------------------------------

// On canvas click, select district
canvas.addEventListener("mousedown", selectDistrict, false);

// Select district event
function selectDistrict(event) {
    var x = event.x;
    var y = event.y;

    var canvas = document.getElementById("map");

    x -= canvas.offsetLeft;
    y -= canvas.offsetTop;

    // Clicked district
    selectedDistrict = collides(districtSizes, x, y);
    updateSelectedDistrict();
}

// Upgrade District, subtracting costs and increasing its level
function upgradeDistrict(event) {
    if (selectedDistrict == "") {
        return;
    }
    var district = districts[selectedDistrict];
    var levelResourceCost = district.levelResourceCost();

    resourceStash.subtract(levelResourceCost);
    districts[selectedDistrict].level += 1;

    updateResourceStash();
    updateSelectedDistrict();
    district.updateImage();
}

// Draw / Update HTML Functions
// ----------------------------------------------------------------------------

// Update info about selected district.
function updateSelectedDistrict() {
    if (selectedDistrict == "") {
        return;
    }
    var district = districts[selectedDistrict];
    var levelResourceCost = district.levelResourceCost();

    // Name
    var districtNameLabel = document.getElementById("districtName");
    districtNameLabel.innerHTML = `<u>${district.name} District: Level ${district.level+1}</u>`;

    // Thumbnail image
    var districtThumbnail = document.getElementById("districtThumbnail");
    districtThumbnail.src = `${district.thumbnailSrc()}`;

    // Description
    var districtDescription = document.getElementById("districtDescription");
    districtDescription.innerHTML = `${district.description}`;

    // Show / Hide Costs if fully upgraded
    var cantUpgrade = ! district.canUpgradeLevel();
    var districtUpgradeCosts = document.getElementById("districtUpgradeCosts");
    districtUpgradeCosts.style.visibility = cantUpgrade ? "hidden" : "visible";

    // Update Upgrade costs, set to black if sufficient, red if not and hide/show if used
    var districtUpgradeGrainCost = document.getElementById("districtUpgradeGrainCost");
    districtUpgradeGrainCost.parentNode.style.visibility = levelResourceCost.food == 0 || cantUpgrade ? "hidden" : "visible";
    districtUpgradeGrainCost.style.color = resourceStash.food >= levelResourceCost.food ? "black" : "red";
    districtUpgradeGrainCost.innerHTML = `${levelResourceCost.food}`;

    var districtUpgradeWoodCost = document.getElementById("districtUpgradeWoodCost");
    districtUpgradeWoodCost.parentNode.style.visibility = levelResourceCost.wood == 0 || cantUpgrade ? "hidden" : "visible";
    districtUpgradeWoodCost.style.color = resourceStash.wood >= levelResourceCost.wood ? "black" : "red";
    districtUpgradeWoodCost.innerHTML = `${levelResourceCost.wood}`;

    var districtUpgradeStoneCost = document.getElementById("districtUpgradeStoneCost");
    districtUpgradeStoneCost.parentNode.style.visibility = levelResourceCost.stone == 0 || cantUpgrade ? "hidden" : "visible";
    districtUpgradeStoneCost.style.color = resourceStash.stone >= levelResourceCost.stone ? "black" : "red";
    districtUpgradeStoneCost.innerHTML = `${levelResourceCost.stone}`;

    var districtUpgradeGoldCost = document.getElementById("districtUpgradeGoldCost");
    districtUpgradeGoldCost.parentNode.style.visibility = levelResourceCost.gold == 0 || cantUpgrade ? "hidden" : "visible";
    districtUpgradeGoldCost.style.color = resourceStash.gold >= levelResourceCost.gold ? "black" : "red";
    districtUpgradeGoldCost.innerHTML = `${levelResourceCost.gold}`;

    // Show / Hide Upgrade Button
    var districtUpgradeButton = document.getElementById('districtUpgradeButton');
    districtUpgradeButton.style.visibility = !resourceStash.sufficient(levelResourceCost) || cantUpgrade ? "hidden" : "visible";
}

// Update selected district info
function updateResourceStash() {
    // Resource stash
    var resourceGrain = document.getElementById("resourceGrain");
    resourceGrain.innerHTML = `${resourceStash.food}`;

    var resourceWood = document.getElementById("resourceWood");
    resourceWood.innerHTML = `${resourceStash.wood}`;

    var resourceStone = document.getElementById("resourceStone");
    resourceStone.innerHTML = `${resourceStash.stone}`;

    var resourceGold = document.getElementById("resourceGold");
    resourceGold.innerHTML = `${resourceStash.gold}`;
}

// Check which rectangle collides
// rects: object of Size instances
function collides(rects, x, y) {
    var isCollision = "";
    for (var key in rects) {
        var rect = rects[key];
        var left = rect.x, right = rect.x + rect.width;
        var top = rect.y, bottom = rect.y + rect.height;
        if (right >= x
            && left <= x
            && bottom >= y
            && top <= y) {
            isCollision = key;
            break;
        }
    }
    return isCollision;
}

pullGameState();

// Data Synchronization
// ----------------------------------------------------------------------------

// Pull Game data from Server
function pullGameState() {
    gameId = $('#Id').val();
    $.ajax({
        url: "/api/GameState/" + gameId,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            var gameState = new GameState(gameId, result);
            gameState.save(resourceStash, districts);
            updateSelectedDistrict();
            updateResourceStash();
        },
        error: function (errormessage) {
            handleSaveErrors(errormessage.responseText);
        }
    });
    updateSelectedDistrict();
    updateResourceStash();
}

// Save Game data to Server
function syncGameState() {
    var gameState = new GameState(gameId);
    gameState.load(resourceStash, districts);
    var test = JSON.stringify(gameState);
    $.ajax({
        url: "/api/GameState/" + gameId,
        data: JSON.stringify(gameState),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            // If need to do anything
        },
        error: function (errormessage) {
            handleSaveErrors(errormessage.responseText);
        }
    });
}

// Function which handles errors
// If has key
function handleSaveErrors(validationErrors) {
    try {
        var errors = JSON.parse(validationErrors);
        for (var key in errors.ModelState) {
            var property = key.replace("contact.", "");
            var message = errors.ModelState[key].join("<br />");
            console.log(message);
        }
    }
    catch (e) {
        console.log(message);
    }
}