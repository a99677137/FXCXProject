require "RootDefine";
GameLog = Game.Tools.CommonTools.GameLog;

--function print(...)
--    local param = {...}
--    local logStr = "";
--    for i=1,#param do
--        local val = param[i];
--        logStr = logStr .. tostring(val) .. "\t"
--    end
--    Game.Tools.CommonTools.GameLog.LuaDebug("<color=#FFFA7D>" .. logStr .. "</color>");
--end

function printe(...)
    local param = {...}
    local logStr = "";
    for i=1,#param do
        local val = param[i];
        logStr = logStr .. tostring(val) .. "\t"
    end
    Game.Tools.CommonTools.GameLog.LuaError("<color=#FF7800>" .. logStr .. "</color>");
end