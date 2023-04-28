#include "mkl.h"

extern "C"  _declspec(dllexport)
void CubicInterpolate(MKL_INT nx, MKL_INT ny, double* x, double* y, double* bc, double* scoeff, MKL_INT nsite,
	double* site, MKL_INT ndorder, MKL_INT * dorder, double* result, int& ret, double* leftLim, double* rightLim, double* intRes);