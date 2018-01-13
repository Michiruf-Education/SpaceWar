uniform float InShowDistance = 0.1;
uniform vec3 InShowColor = vec3(1.0, 0.0, 0.0);

varying vec2 VertexPosition;
varying vec2 PlayerPosition;

void main (void) {
    float distance = length(VertexPosition - PlayerPosition);
    float showPercentace = 1.0 - distance / InShowDistance;
    if(showPercentace > 0.0) {
        gl_FragColor = vec4(InShowColor, showPercentace);
        //gl_FragColor = vec4(OutVertexPosition.x, OutVertexPosition.y, 0.8, 1.0);
    } 
}
