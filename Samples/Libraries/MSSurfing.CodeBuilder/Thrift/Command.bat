@echo ��������Thrift�ļ�
@echo off
	for /r %%i in (*.tf) do thrift-0.11.0.exe --gen csharp "%%i"

	::for /r %%i in (*.tf) do thrift-0.9.2.exe --gen csharp:async "%%i"
	::thrift-0.9.2.exe --gen csharp Orders\TOrder.tf
	::thrift-0.9.2.exe --gen csharp Orders\TOrderService.tf
	::�� ����Ҫ�� ʹ�ù���Ա��ʽִ��
@echo ���ɽ���
@pause