precision highp float;

uniform float InShowDistance;
uniform vec3 InShowColor;

varying vec2 VertexPosition;
varying vec2 PlayerPosition;

void main (void) {
    float distance = length(VertexPosition - PlayerPosition);
    float showPercentace = 1.0 - distance / InShowDistance;
    if(showPercentace > 0.0 && showPercentace <= 1.0) {
        gl_FragColor = vec4(InShowColor, showPercentace);
    }

//    gl_FragColor = vec4(VertexPosition, 0.0, 1.0);    
}
