attribute vec2 InPlayerPosition;

varying vec2 VertexPosition;
varying vec2 PlayerPosition;

void main() {
	gl_Position = ftransform();
	
	// Pass through varyings
	VertexPosition = gl_Vertex.xy;
	PlayerPosition = InPlayerPosition; 
}
