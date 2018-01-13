precision highp float;

attribute vec2 InCameraPosition;
attribute vec2 InPlayerPosition;

varying vec2 VertexPosition;
varying vec2 PlayerPosition;

vec2 multiplyMatrixVector(in vec2 vector, in mat3 matrix) {
    // From FastVector2Transform.cs
    //vector.x * matrix.M11 + vector.y * matrix.M21 + matrix.M31
    //vector.x * matrix.M12 + vector.y * matrix.M22 + matrix.M32
    return(vec2(
        vector.x * matrix[0][0] + vector.y * matrix[0][1] + matrix[0][2],
        vector.x * matrix[1][0] + vector.y * matrix[1][1] + matrix[1][2]
    ));
}

void main() {
	gl_Position = ftransform();
	
	// Pass through varyings
	// Currently the camera is included in the borders vertex parameters.
	// Because of this we need to revert this data or also add it to the player.
	// The camera data is inverted, so there is not a "+", but a "-"!
	VertexPosition = gl_Vertex.xy;
	PlayerPosition = InPlayerPosition - InCameraPosition;
}
