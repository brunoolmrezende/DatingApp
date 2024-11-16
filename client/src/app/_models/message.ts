export interface Message {
    id: number
    recipientId: number
    recipientUsername: string
    recipientPhotoUrl: string
    senderId: number
    senderUsername: string
    senderPhotoUrl: string
    content: string
    dateRead?: Date
    dateSent: Date
  }
  