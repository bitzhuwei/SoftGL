﻿namespace CSharpGL
{
    /// <summary>
    /// uniform int variable[10];
    /// </summary>
    public class UniformInt32Array : UniformArrayVariable<int>
    {
        /// <summary>
        /// uniform int variable[10];
        /// </summary>
        /// <param name="varName"></param>
        /// <param name="length"></param>
        public UniformInt32Array(string varName, int length) : base(varName, length) { }

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