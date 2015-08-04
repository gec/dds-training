/* Convenience header file to be included by applications
   that want to use the COW C++11 types */

#ifdef _WIN32
#define RTI_WIN32
#define NDDS_DLL_VARIABLE
#endif

/* DDS modern C++ API */
#include <dds/dds.hpp>
/* Generated types */
#include "generated/cow.hpp"
