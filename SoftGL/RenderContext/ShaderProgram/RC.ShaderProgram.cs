using System;
using System.Collections.Generic;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private uint nextShaderProgramName = 1;

        /// <summary>
        /// name -> ShaderProgram object.
        /// </summary>
        private readonly Dictionary<uint, ShaderProgram> nameShaderProgramDict = new Dictionary<uint, ShaderProgram>();

        private ShaderProgram currentShaderProgram = null;

        public static uint glCreateProgram()
        {
            uint id = 0;
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                id = context.CreateProgram();
            }

            return id;
        }

        private uint CreateProgram()
        {
            uint name = nextShaderProgramName;
            var program = new ShaderProgram(name);
            this.nameShaderProgramDict.Add(name, program);
            nextShaderProgramName++;

            return name;
        }

        public static void glAttachShader(uint program, uint shader)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.AttachShader(program, shader);
            }
        }

        private void AttachShader(uint programName, uint shaderName)
        {
            if (programName == 0 || shaderName == 0) { SetLastError(ErrorCode.InvalidValue); return; }
            if ((!this.nameShaderProgramDict.ContainsKey(programName)) || (!this.nameShaderDict.ContainsKey(shaderName))) { SetLastError(ErrorCode.InvalidOperation); return; }

            ShaderProgram program = this.nameShaderProgramDict[programName];
            Shader shader = this.nameShaderDict[shaderName];
            if (program.AttachedShaders.Contains(shader)) { SetLastError(ErrorCode.InvalidOperation); return; }

            program.AttachedShaders.Add(shader);
        }

        public static void glLinkProgram(uint name)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.LinkProgram(name);
            }
        }

        private void LinkProgram(uint name)
        {
            if (name == 0) { SetLastError(ErrorCode.InvalidValue); return; }
            if (!this.nameShaderProgramDict.ContainsKey(name)) { SetLastError(ErrorCode.InvalidOperation); return; }
            ShaderProgram program = this.nameShaderProgramDict[name];
            // TODO: GL_INVALID_OPERATION is generated if program​ is the currently active program object and transform feedback mode is active.

            program.Link();
        }

        public static int glGetAttribLocation(uint program, string name)
        {
            int result = -1;
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                result = context.GetAttribLocation(program, name);
            }

            return result;
        }

        private int GetAttribLocation(uint program, string name)
        {
            if ((program == 0) || (!this.nameShaderProgramDict.ContainsKey(program))) { SetLastError(ErrorCode.InvalidOperation); return -1; }
            // TODO: GL_INVALID_OPERATION is generated if program​ has not been successfully linked.

            ShaderProgram shaderProgram = this.nameShaderProgramDict[program];
            return shaderProgram.GetAttribLocation(name);
        }

        public static void glUseProgram(uint program)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.UseProgram(program);
            }
        }

        private void UseProgram(uint name)
        {
            if (name == 0) { SetLastError(ErrorCode.InvalidValue); return; }
            if (!this.nameShaderProgramDict.ContainsKey(name)) { SetLastError(ErrorCode.InvalidOperation); return; }
            ShaderProgram program = this.nameShaderProgramDict[name];
            if (program.LogInfo != string.Empty) { SetLastError(ErrorCode.InvalidOperation); return; }
            // TODO: GL_INVALID_OPERATION is generated if transform feedback mode is active.

            this.currentShaderProgram = program;
        }

        public static void glGetProgramiv(uint program, uint pname, int[] pValues)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.glGetProgramiv(program, (GetProgramivPName)pname, pValues);
            }
        }

        private void glGetProgramiv(uint name, GetProgramivPName pname, int[] pValues)
        {
            if (!this.nameShaderProgramDict.ContainsKey(name)) { SetLastError(ErrorCode.InvalidValue); return; }
            ShaderProgram program = this.nameShaderProgramDict[name];
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            if (program.LogInfo != string.Empty) { SetLastError(ErrorCode.InvalidOperation); return; }
            if (!Enum.IsDefined(typeof(GetProgramivPName), pname)) { SetLastError(ErrorCode.InvalidEnum); return; }
            if (pname == GetProgramivPName.GeometryVerticesOut
                || pname == GetProgramivPName.GeometrInputType
                || pname == GetProgramivPName.GeometryOutputType)
            {
                if (program.GeometryShader == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            }
            if (pname == GetProgramivPName.ComputeWorkGroupSize && program.ComputeShader == null)
            { SetLastError(ErrorCode.InvalidOperation); return; }

            switch (pname) // TODO: fill in the blanks of glGetProgramiv(..).
            {
                case GetProgramivPName.DeleteSataus:
                    break;
                case GetProgramivPName.LinkStatus:
                    if (pValues != null && pValues.Length > 0) { pValues[0] = (int)(program.LogInfo == string.Empty ? GL.GL_TRUE : GL.GL_FALSE); }
                    break;
                case GetProgramivPName.ValidateStatus:
                    break;
                case GetProgramivPName.InfoLogLength:
                    break;
                case GetProgramivPName.AttachedShaders:
                    break;
                case GetProgramivPName.ActiveAtmoicCounterBuffers:
                    break;
                case GetProgramivPName.ActiveAttributes:
                    break;
                case GetProgramivPName.ActiveAttributeMaxLength:
                    break;
                case GetProgramivPName.ActiveUniforms:
                    break;
                case GetProgramivPName.ActiveUniformBlocks:
                    break;
                case GetProgramivPName.ActiveUniformBlockMaxNameLength:
                    break;
                case GetProgramivPName.ActiveUniformMaxLength:
                    break;
                case GetProgramivPName.ComputeWorkGroupSize:
                    break;
                case GetProgramivPName.ProgramBinaryLength:
                    break;
                case GetProgramivPName.TransformFeedbackBufferMode:
                    break;
                case GetProgramivPName.TransformFeedbackVaryings:
                    break;
                case GetProgramivPName.TransformFeedbackVaryingMaxLength:
                    break;
                case GetProgramivPName.GeometryVerticesOut:
                    break;
                case GetProgramivPName.GeometrInputType:
                    break;
                case GetProgramivPName.GeometryOutputType:
                    break;
                default:
                    throw new NotDealWithNewEnumItemException(typeof(GetProgramivPName));
            }
        }
    }

    enum GetProgramivPName : uint
    {
        DeleteSataus = GL.GL_DELETE_STATUS,
        LinkStatus = GL.GL_LINK_STATUS,
        ValidateStatus = GL.GL_VALIDATE_STATUS,
        InfoLogLength = GL.GL_INFO_LOG_LENGTH,
        AttachedShaders = GL.GL_ATTACHED_SHADERS,
        ActiveAtmoicCounterBuffers = GL.GL_ACTIVE_ATOMIC_COUNTER_BUFFERS,
        ActiveAttributes = GL.GL_ACTIVE_ATTRIBUTES,
        ActiveAttributeMaxLength = GL.GL_ACTIVE_ATTRIBUTE_MAX_LENGTH,
        ActiveUniforms = GL.GL_ACTIVE_UNIFORMS,
        ActiveUniformBlocks = GL.GL_ACTIVE_UNIFORM_BLOCKS,
        ActiveUniformBlockMaxNameLength = GL.GL_ACTIVE_UNIFORM_BLOCK_MAX_NAME_LENGTH,
        ActiveUniformMaxLength = GL.GL_ACTIVE_UNIFORM_MAX_LENGTH,
        ComputeWorkGroupSize = GL.GL_COMPUTE_WORK_GROUP_SIZE,
        ProgramBinaryLength = GL.GL_PROGRAM_BINARY_LENGTH,
        TransformFeedbackBufferMode = GL.GL_TRANSFORM_FEEDBACK_BUFFER_MODE,
        TransformFeedbackVaryings = GL.GL_TRANSFORM_FEEDBACK_VARYINGS,
        TransformFeedbackVaryingMaxLength = GL.GL_TRANSFORM_FEEDBACK_VARYING_MAX_LENGTH,
        GeometryVerticesOut = GL.GL_GEOMETRY_VERTICES_OUT,
        GeometrInputType = GL.GL_GEOMETRY_INPUT_TYPE,
        GeometryOutputType = GL.GL_GEOMETRY_OUTPUT_TYPE
    }
}
