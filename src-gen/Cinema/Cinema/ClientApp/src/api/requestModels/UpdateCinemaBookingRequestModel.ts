import {Client} from "../models/customers/Client"
import {Seat} from "../models/resources/Seat"
import {NightPlan} from "../models/schedules/NightPlan"
export type UpdateCinemaBookingRequestModel = {
	id: string
	name: string
	client: Client
	seat: Seat
	plan: NightPlan
} 
