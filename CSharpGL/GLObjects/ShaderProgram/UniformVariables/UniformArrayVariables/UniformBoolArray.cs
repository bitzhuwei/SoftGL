﻿namespace CSharpGL
{
    /// <summary>
    /// uniform bool variable[10];
    /// </summary>
    public class UniformBoolArray : UniformArrayVariable<bool>
    {
        /// <summary>
        /// uniform bool variable[10];
        /// </summary>
        /// <param name="varName"></param>
        /// <param name="length"></param>
        public UniformBoolArray(string varName, int length) : base(varName, length) { }

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