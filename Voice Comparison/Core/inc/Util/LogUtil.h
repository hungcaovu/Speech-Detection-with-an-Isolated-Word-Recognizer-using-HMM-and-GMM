#pragma once

#include <string>
#include <stdarg.h>
#include <time.h>
#include "Common.h"
#include "Mutex.h"
class Logger
{
private:
	static std::mutex m_mutex;
	static std::string m_logfile;
	static int m_mode;
	static FILE *fp_da;
	static FILE *fp_d;
	static FILE *fp_i;
	static FILE *fp_s;
	static FILE *fp;
	static void MoveFile(std::string file);
	static void OpenFile(LOG_LEVEL level, std::string file);
	static FILE * &GetFile(LOG_LEVEL level);
public:
	static void SetEnabled(int enable);
	static void SeparateLog();
	static void SetFilename(std::string logfile) { m_logfile = logfile; };
	static void Print(int level, char *fmt, ...);
	static void Log(LOG_LEVEL level, char *fmt, ...);
};