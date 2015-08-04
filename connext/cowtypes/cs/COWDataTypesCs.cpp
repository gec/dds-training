#ifdef _WIN32
#define RTI_WIN32
#define NDDS_DLL_VARIABLE
#define NDDS_USER_DLL_EXPORT
#pragma warning(push)
#pragma warning(disable:4100) // warning C4100: unreferenced formal parameter
#pragma warning(disable:4127) // warning C4127: conditional expression is constant
#pragma warning(disable:4244) // warning C4244: 'argument' : conversion from 'int' to 'DDS_Boolean', possible loss of data
#pragma warning(disable:4701) // warning C4701: potentially uninitialized local variable 'enum_tmp' used
#endif

#include "generated/cow.cpp"
#include "generated/cowSupport.cpp"
#include "generated/cowPlugin.cpp"

#ifdef _WIN32
#pragma warning(pop)
#endif
