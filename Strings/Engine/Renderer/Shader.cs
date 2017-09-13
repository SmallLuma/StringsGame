﻿using OpenTK.Graphics.ES20;
using System;
using System.IO;
using Android.Util;

namespace Strings.Engine.Renderer
{
    class Shader
    {
        private int CompileShader(string shaderName,ShaderType type)
        {
            var src = new StreamReader(GameLoop.Context.Assets.Open("Shaders/" + shaderName + ".glsl")).ReadToEnd();
            var id = GL.CreateShader(type);
            GL.ShaderSource(id, src);
            GL.CompileShader(id);
            var error = GL.GetShaderInfoLog(id);

            if (error != string.Empty) throw new System.Exception(error);
            return id;
        }

        public Shader(string vsName,string fsName)
        {
            try
            {
                shaderID = GL.CreateProgram();

                var vsID = CompileShader(vsName + ".vs", ShaderType.VertexShader);
                var fsID = CompileShader(fsName + ".fs", ShaderType.FragmentShader);
                GL.AttachShader(shaderID, vsID);
                GL.AttachShader(shaderID, fsID);
                GL.LinkProgram(shaderID);

                var error = GL.GetProgramInfoLog(shaderID);
                if (error != string.Empty) throw new Exception(error);
            }
            catch(Exception ex)
            {
                Log.Error("Shader Error", ex.Message);
            }
        }

        public void Use()
        {
            GL.UseProgram(shaderID);
        }

        public static void Unuse()
        {
            GL.UseProgram(0);
        }

        int shaderID;
    }
}