Shader "Lines/Colored Blended"
{
	SubShader 
	{ 
		Pass 
		{ 
			Blend SrcAlpha OneMinusSrcAlpha 
			
			BindChannels 
			{ 
				Bind "Color",color 
			} 
			ZWrite On
			ZTest On
			Cull Front 
			Fog 
			{
				Mode Off 
			} 
		}
	}
}