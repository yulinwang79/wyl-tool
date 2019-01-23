#ifndef __RING_BUFFER_H__
#define __RING_BUFFER_H__

#define RING_BUF_SIZE	65536	//64k,big buffer,should be 2^n

#define ringbuf_get_roomleft(_ring_buffer,_left)   \
{\
    if ( _ring_buffer.rIndex > _ring_buffer.wIndex ) \
	{\
		_left = _ring_buffer.rIndex - _ring_buffer.wIndex;\
	}\
	else\
	{\
		_left = RING_BUF_SIZE - _ring_buffer.wIndex + _ring_buffer.rIndex;\
	}\
}

#define ringbuf_get_content_size(_ring_buffer,_len)\
{\
	if ( _ring_buffer.rIndex > _ring_buffer.wIndex ) \
	{\
		_len = RING_BUF_SIZE - _ring_buffer.rIndex + _ring_buffer.wIndex;\
	}\
	else\
	{\
		_len = _ring_buffer.wIndex - _ring_buffer.rIndex;\
	}\
}

#define ringbuf_put(_ring_buffer,_pdata,_len) \
{\
	if ( _ring_buffer.rIndex > _ring_buffer.wIndex ) \
	{\
		memcpy(_ring_buffer.aucBuf+_ring_buffer.wIndex,_pdata,_len);\
	}\
	else\
	{\
		if(RING_BUF_SIZE - _ring_buffer.wIndex < _len)\
		{\
			memcpy(_ring_buffer.aucBuf + _ring_buffer.wIndex,_pdata,RING_BUF_SIZE - _ring_buffer.wIndex);\
			memcpy(_ring_buffer.aucBuf,((unsigned char *)(_pdata)) + RING_BUF_SIZE - _ring_buffer.wIndex,_len + _ring_buffer.wIndex - RING_BUF_SIZE);\
		}\
		else\
		{\
			memcpy(_ring_buffer.aucBuf+_ring_buffer.wIndex,_pdata,_len);\
		}\
	}\
	_ring_buffer.wIndex += _len;\
	_ring_buffer.wIndex &= ~RING_BUF_SIZE;\
}

typedef struct {
	unsigned char *aucBuf;
	int wIndex;
	int rIndex;
}xRingBuffer;

#endif

