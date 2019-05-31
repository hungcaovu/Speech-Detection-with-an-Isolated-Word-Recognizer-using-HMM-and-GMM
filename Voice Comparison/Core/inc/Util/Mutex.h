#include <mutex>
class Locker
{
public:
	Locker(std::mutex & mutex) : m_mutex(mutex)
	{
		m_mutex.lock();
	}

	~Locker()
	{
		m_mutex.unlock();
	}

private:
	std::mutex& m_mutex;
};