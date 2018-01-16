#version 130

uniform float InShowDistance;
uniform vec3 InShowColor;
uniform vec2 EaseStart;
uniform vec2 EaseEnd;
uniform vec2 EaseP1;
uniform vec2 EaseP2;

uniform vec2 InPlayerPosition;
varying vec2 VertexPosition;

vec2 toBezier(in float x, in vec2 P0, in vec2 P1, in vec2 P2, in vec2 P3) {
    // @see https://vicrucann.github.io/tutorials/bezier-shader/
    // Modified for 2d by using just vec2 instead of vec4
    float x2 = x * x;
    float one_minus_x = 1.0 - x;
    float one_minus_x2 = one_minus_x * one_minus_x;
    return(
        P0 * one_minus_x2 * one_minus_x +
        P1 * 3.0 * x * one_minus_x2 +
        P2 * 3.0 * x2 * one_minus_x +
        P3 * x2 * x
    );
}

float ease(in float value) {
    return(toBezier(value, EaseStart, EaseP1, EaseP2, EaseEnd).y);
}

float alpha() {
    float result = 0;
    // TODO Loop disabled because arrays do not work yet
    //for(int i = 0; i < 4; i++) {
        float distance = length(VertexPosition - InPlayerPosition);
        float showPercentace = 1.0 - distance / InShowDistance;
        result = max(result, showPercentace);
    //}
    return ease(result);
}

void main(void) {
    gl_FragColor = vec4(InShowColor, alpha());
}

// TODO: When a shot hits the border, a small light blink would be nice in the border!
