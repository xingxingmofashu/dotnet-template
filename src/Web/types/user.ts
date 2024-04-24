export interface User {
    id: bigint
    name?: string
    account?: string
    password?: string
    status: number
    gender: number
    phone?: string
    email?: string
    note?: string
    supervisorId?: bigint
}