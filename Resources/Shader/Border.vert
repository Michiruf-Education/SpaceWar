#version 120

varying vec2 VertexPosition;

void main() {
	gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
	
	// Pass through varyings
	VertexPosition = gl_Vertex.xy;
}
