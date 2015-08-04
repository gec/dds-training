#ifdef _WIN32
#define RTI_WIN32
#define NDDS_DLL_VARIABLE
#define NDDS_USER_DLL_EXPORT

#pragma warning(push)
#pragma warning(disable:4100) // warning C4100: unreferenced formal parameter
#pragma warning(disable:4127) // warning C4127: conditional expression is constant
#pragma warning(disable:4244) // warning C4244: 'argument' : conversion from 'int' to 'DDS_Boolean', possible loss of data
#pragma warning(disable:4701) // warning C4701: potentially uninitialized local variable 'enum_tmp' used
#endif /* _WIN32 */

#include "generated/cow.c"
#include "generated/cowPlugin.c"
#include "generated/cowSupport.c"

#ifdef _WIN32
#pragma warning(pop)
#endif /* _WIN32 */