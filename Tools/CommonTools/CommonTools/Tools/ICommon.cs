using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Tools.CommonTools
{
    public interface ICommon
    {
        void Init();
        void Tick(uint uDeltaTimeMS);
        void Release();

        //大多数这个方法没什么卵用
        void Destroy();
    }
}
