﻿using System;
using System.Collections.Generic;
using System.Text;
using MoonSharp.Interpreter;
using Microsoft.Xna.Framework;

namespace Barotrauma
{

	public static class LuaCustomConverters
	{

		public static void RegisterAll()
		{
            RegisterAction<Item>();
            RegisterAction<Character>();
            RegisterAction<Entity>();
            RegisterAction();

#if CLIENT
            Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function, typeof(GUIButton.OnClickedHandler), v =>
            {

                var function = v.Function;
                return (GUIButton.OnClickedHandler)((GUIButton a, object b) => new LuaResult(LuaSetup.luaSetup.CallFunction(function, a, b)).Bool());
            });

            Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function, typeof(GUITextBox.OnTextChangedHandler), v =>
            {
                var function = v.Function;
                return (GUITextBox.OnTextChangedHandler)((GUITextBox a, string b) => new LuaResult(LuaSetup.luaSetup.CallFunction(function, a, b)).Bool());
            });

            Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function, typeof(GUITextBox.OnEnterHandler), v =>
            {
                var function = v.Function;
                return (GUITextBox.OnEnterHandler)((GUITextBox a, string b) => new LuaResult(LuaSetup.luaSetup.CallFunction(function, a, b)).Bool());
            });
#endif
        }


        public static void RegisterAction<T>()
        {
            Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function, typeof(Action<T>), v =>
            {
                var function = v.Function;
                return (Action<T>)(p => function.Call(p));
            });
        }

        public static void RegisterAction()
        {
            Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function, typeof(Action), v => 
            {
                var function = v.Function;
                return (Action)(() => function.Call());
            });
        }

    }

}
