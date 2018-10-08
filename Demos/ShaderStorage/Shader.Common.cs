namespace SoftGL
{
    struct Material
    {
        /// <summary>
        /// ability to reflect ambient color.[0, 1]
        /// </summary>
        public vec3 ambient;
        /// <summary>
        /// ability to reflect diffuse color.[0, 1]
        /// </summary>
        public vec3 diffuse;
        /// <summary>
        /// ability to reflect specular color.[0, 1]
        /// </summary>
        public vec3 specular;
        /// <summary>
        /// Shiness.
        /// </summary>
        public float shiness;
        ///// <summary>
        ///// Emission.
        ///// </summary>
        //public vec3 emission;
        /// <summary>
        /// 
        /// </summary>
        public float strength;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ambient"></param>
        /// <param name="diffuse"></param>
        /// <param name="specular"></param>
        /// <param name="shiness"></param>
        /// <param name="strength"></param>
        public Material(vec3 ambient, vec3 diffuse, vec3 specular, float shiness, float strength)
        {
            this.ambient = ambient; this.diffuse = diffuse; this.specular = specular;
            this.shiness = shiness; this.strength = strength;
        }

        public override string ToString()
        {
            return string.Format("diffuse:{0}", this.diffuse);
        }
    }

    struct SpotLight
    {
        /// <summary>
        /// ability to reflect diffuse color.[0, 1]
        /// </summary>
        public vec3 diffuse;
        /// <summary>
        /// ability to reflect specular color.[0, 1]
        /// </summary>
        public vec3 specular;

        /// <summary>
        /// light's position.
        /// </summary>
        public vec3 position;
        /// <summary>
        /// light's direction from light source to vertex.
        /// </summary>
        public vec3 direction;
        /// <summary>
        /// 
        /// </summary>
        public float cutoff;
        /// <summary>
        /// 
        /// </summary>
        public float exponent;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="diffuse"></param>
        /// <param name="specular"></param>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        /// <param name="cutoff"></param>
        /// <param name="exponent"></param>
        public SpotLight(vec3 diffuse, vec3 specular, vec3 position, vec3 direction, float cutoff, float exponent)
        {
            this.diffuse = diffuse; this.specular = specular;
            this.position = position; this.direction = direction;
            this.cutoff = cutoff; this.exponent = exponent;
        }
    }
}
