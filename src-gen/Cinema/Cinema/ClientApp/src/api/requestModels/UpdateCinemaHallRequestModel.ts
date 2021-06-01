import {Client} from "../models/customers/Client"
export type UpdateCinemaHallRequestModel = {
	id: string
	name: string
	city: string
	Imax: boolean
	cli: Client
} 
