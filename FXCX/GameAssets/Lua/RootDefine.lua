require "Manager/LoginManager";

local function InitGame()
    LoginManager:Init();
end

function Start()
    InitGame();
end

