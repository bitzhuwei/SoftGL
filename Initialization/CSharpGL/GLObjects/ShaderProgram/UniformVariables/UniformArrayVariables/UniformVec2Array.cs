﻿namespace CSharpGL
{
    /// <summary>
    /// uniform vec2 variable[10];
    /// </summary>
    public class UniformVec2Array : UniformArrayVariable<vec2>
    {
        /// <summary>
        /// uniform vec2 variable[10];
        /// </summary>
        /// <param name="varName"></param>
        /// <param name="length"></param>
        public UniformVec2Array(string varName, int length) : base(varName, length) { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="program"></param>
        protected override void DoSetUniform(ShaderProgram program)
        {
            this.Location = program.glUniform(VarName, this.Value.Array);
            this.Updated = false;
        }
    }
}