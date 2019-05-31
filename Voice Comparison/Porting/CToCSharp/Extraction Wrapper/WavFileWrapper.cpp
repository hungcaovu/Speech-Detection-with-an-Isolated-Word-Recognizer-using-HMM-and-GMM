#include <WavFileWrapper.h>

namespace ExtractionWrapper{
bool WavFileWrapper::Load(){
		isValid = wav->Load();
		if (isValid){
			sampleRate = wav->GetSampleRate();
			fullData = UtilWrapper::ConverPtrToList(wav->GetFullData(), wav->GetFullLength());
			selectedData = fullData;
			isSelected = true;
		}
		return isValid;
	}

WavFileWrapper::WavFileWrapper(String ^ path){
		isValid = false;
		isSelected = false;
		this->path = path;
		pin_ptr<const wchar_t> wch = PtrToStringChars(path);
		size_t convertedChars = 0;
		size_t  sizeInBytes = ((path->Length + 1) * 2);
		errno_t err = 0;
		char *str = (char *)malloc(sizeInBytes);
		
		err = wcstombs_s(&convertedChars,
			str, sizeInBytes,
			wch, sizeInBytes);
		if (err == 0){
			wav = new WavFile(str);
		}
	};

bool WavFileWrapper::SelectedWave(COUNT_TYPE start, COUNT_TYPE end){
	isSelected = wav->SelectedWave(start, end);
	if (isSelected){
		selectedData = UtilWrapper::ConverPtrToList(wav->GetSelectedData(), wav->GetSelectedLength());
	}
	return isSelected;
}

bool WavFileWrapper::NormalizeWave(DATA_TYPE peak){
	return wav->NormalizeWave(peak);
}

DATA_TYPE WavFileWrapper::Peak(){
	return wav->Peak();
}

void WavFileWrapper::ShifToZero(){
	wav->ShifToZero();
}

TIME_TYPE WavFileWrapper::GetSelectedTimeLength(){
	return wav->GetSelectedTimeLength();
}
TIME_TYPE WavFileWrapper::GetFullTimeLength(){
	return wav->GetFullTimeLength();
}
};