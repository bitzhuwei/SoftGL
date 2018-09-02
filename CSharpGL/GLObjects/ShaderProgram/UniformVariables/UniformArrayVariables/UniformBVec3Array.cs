﻿namespace CSharpGL
{
    /// <summary>
    /// uniform bvec3 variable[10];
    /// </summary>
    public class UniformBVec3Array : UniformArrayVariable<bvec3>
    {
        /// <summary>
        /// uniform bvec3 variable[10];
        /// </summary>
        /// <param name="varName"></param>
        /// <param name="length"></param>
        public UniformBVec3Array(string varName, int length) : base(varName, length) { }

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