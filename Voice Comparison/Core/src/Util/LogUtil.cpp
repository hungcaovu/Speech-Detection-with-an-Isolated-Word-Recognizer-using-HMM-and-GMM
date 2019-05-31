#include <Util/LogUtil.h>
std::mutex Logger::m_mutex;
std::string Logger::m_logfile = "Log.txt";
int Logger::m_mode = 0;
FILE *Logger::fp_da = NULL;
FILE *Logger::fp_d = NULL;
FILE *Logger::fp_i = NULL;
FILE *Logger::fp_s = NULL;
FILE *Logger::fp = NULL;

void Logger::SeparateLog(){
	Logger::SetEnabled(m_mode);
	Logger::SetEnabled(m_mode);
}

void Logger::MoveFile(std::string file){
	char newname[512];
	time_t t_now = time(NULL);
	tm tm_now = *localtime(&t_now);

	_snprintf_s(newname, sizeof(newname), _TRUNCATE, "%s (renamed at %04d-%02d-%02d %02d-%02d-%02d)",
		file.c_str(),
		tm_now.tm_year + 1900, tm_now.tm_mon + 1, tm_now.tm_mday,
		tm_now.tm_hour, tm_now.tm_min, tm_now.tm_sec);
	newname[sizeof(newname) - 1] = '\0';

	rename(file.c_str(), newname);
}
void Logger::OpenFile(LOG_LEVEL level, std::string file){
	FILE *&fp = GetFile(level);
	if ((fp = _fsopen(file.c_str(), "a+", _SH_DENYWR)) == NULL)
		fp = NULL;
}
void Logger::SetEnabled(int mode)
{
	printf("Log Set Mode: %x", mode);
	m_mode = mode;

	if (fp_da != NULL){
		fclose(fp_da);
		fp_da = NULL;
	}

	if (fp_d != NULL){
		fclose(fp_d);
		fp_d = NULL;
	}

	if (fp_i != NULL){
		fclose(fp_i);
		fp_i = NULL;
	}

	if (fp_s != NULL){
		fclose(fp_s);
		fp_s = NULL;
	}


	if (fp != NULL){
		fclose(fp);
		fp = NULL;
	}

	MoveFile("DATA" + m_logfile);
	MoveFile("DETAIL" + m_logfile);
	MoveFile("INFORMATION" + m_logfile);
	MoveFile("STEP" + m_logfile);

	if (mode & DATA){
		OpenFile(DATA, "DATA" + m_logfile);
		return;
	}

	if (mode & DETAIL){
		OpenFile(DETAIL, "DETAIL" + m_logfile);
		return;
	}

	if (mode & INFORMATION){
		OpenFile(INFORMATION, "INFORMATION" + m_logfile);
		return;
	}

	if (mode & STEP){
		OpenFile(STEP, "STEP" + m_logfile);
		return;
	}
};

FILE * &Logger::GetFile(LOG_LEVEL level){
	/*switch (level){
	case DATA:
		return fp_da;
		break;
	case DETAIL:
		return fp_d;
		break;
	case INFORMATION:
		return fp_i;
		break;
	case STEP:
		return fp_s;
		break;
	}*/
	return fp;
}

void Logger::Print(int level, char *fmt, ...){
	FILE *&fp = GetFile((LOG_LEVEL)m_mode);

	if (fp == NULL) return;

	if (!(level & m_mode)) return;

	char buffer[8096];
	va_list va;

	Locker l(m_mutex);
	if (!(level & NONE_TIME)){
		time_t t1;
		struct tm t2;
		time(&t1);
		localtime_s(&t2, &t1);
		fprintf(fp, "%04d-%02d-%02d %02d:%02d:%02d | ", t2.tm_year + 1900, t2.tm_mon + 1, t2.tm_mday, t2.tm_hour, t2.tm_min, t2.tm_sec);
	}
	
	va_start(va, fmt);
	_vsnprintf_s(buffer, sizeof(buffer), _TRUNCATE, fmt, va);
	va_end(va);
	fprintf(fp, "%s", buffer);

	fflush(fp);
}
/*void Logger::Log(LOG_LEVEL level, char *fmt, ...)
{
	FILE *&fp = GetFile(level);

	if (fp == NULL) return;
	char buffer[4096];
	va_list va;
	time_t t1;
	struct tm t2;

	Locker l(m_mutex);

	time(&t1);
	localtime_s(&t2, &t1);
	

	fprintf(fp, "%04d-%02d-%02d %02d:%02d:%02d | ", t2.tm_year + 1900, t2.tm_mon + 1, t2.tm_mday, t2.tm_hour, t2.tm_min, t2.tm_sec);

	va_start(va, fmt);
	_vsnprintf_s(buffer, sizeof(buffer), _TRUNCATE, fmt, va);
	va_end(va);
	fprintf(fp, "%s\n", buffer);

	fflush(fp);
}
*/