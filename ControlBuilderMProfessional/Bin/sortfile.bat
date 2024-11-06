

if EXIST tmp%1  del tmp%1
call sort < %1 > tmp%1

if EXIST tmp%1 copy tmp%1 %1

del tmp%1

exit
