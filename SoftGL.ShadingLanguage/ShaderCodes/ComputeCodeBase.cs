using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    public abstract partial class ComputeCodeBase : CodeBase
    {
        /// <summary>
        /// 这个常量的数值就是在布局限定符 local_size_x、local_size_y 和 local_size_z 中指定的数值。创建这个常量有两个目的。首先，它使得本地工作组的大小可以在 Shader 中被多次访问而无需依赖预处理。其次，它使得以多维形式表示的本地工作组大小可以直接按向量处理，而无须显式地构造。
        /// </summary>
        public readonly uvec3 gl_WorkGroupSize;
        /// <summary>
        /// 这个向量记录的是传递给 glDispatchCompute(..)的参数（num_groups_x、num_groups_y 和num_groups_z）。在 Shader 中可以直接用此变量获取全局工作组的大小。 
        /// </summary>
        public readonly uvec3 gl_NumWorkGroups;
        /// <summary>
        /// 这个向量表示当前工作项在本地工作组中的位置，其范围为从 uvec3(0)到(gl_WorkGroupSize - uvec3(1))。 
        /// </summary>
        public readonly uvec3 gl_LocalInvocationID;
        /// <summary>
        /// 这个向量表示当前本地工作组在全局工作组中的位置，其范围为从 uvec3(0)到(gl_NumWorkGroups - uvec3(1))。 
        /// </summary>
        public readonly uvec3 gl_WorkGroupID;
        /// <summary>
        /// 这个向量表示当前工作项在全局工作组中的位置，其值为(gl_WorkGroupID * gl_WorkGroupSize + gl_LocalInvocationID)。 
        /// </summary>
        public readonly uvec3 gl_GlobalInvocationID;
        /// <summary>
        /// 这个数值是将 gl_LocalInvocationID 扁平化的结果，其值为(gl_LocalInvocationID.z * gl_WorkGroupSize.y * glWorkGroupSize.x + gl_LocalInvocationID.y * gl_WorkGroupSize.x + gl_LocalInvocationID.x)。
        /// </summary>
        public readonly uint gl_LocalInvocationIndex;

        public abstract void main();
    }
}
