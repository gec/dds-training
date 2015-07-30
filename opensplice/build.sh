idlpp -lisocpp -d generated ../idl/cow.idl
g++ pv.cpp generated/*.cpp -I$OSPL_HOME/include -I$OSPL_HOME/include/sys -I$OSPL_HOME/include/dcps/C++/isocpp -I$OSPL_HOME/include/dcps/C++/SACPP -L$OSPL_HOME/lib -ldcpsisocpp -lddskernel -o pv
g++ battery.cpp generated/*.cpp -I$OSPL_HOME/include -I$OSPL_HOME/include/sys -I$OSPL_HOME/include/dcps/C++/isocpp -I$OSPL_HOME/include/dcps/C++/SACPP -L$OSPL_HOME/lib -ldcpsisocpp -lddskernel -o battery
