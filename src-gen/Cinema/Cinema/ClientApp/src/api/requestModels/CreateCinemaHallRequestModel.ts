import {Client} from "../models/customers/Client"
export type CreateCinemaHallRequestModel = {
	name: string
	city: string
	Imax: boolean
	cli: Client
} 
