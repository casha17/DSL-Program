import {Client} from "../customers/Client"
import {Seat} from "../resources/Seat"
import {NightPlan} from "../schedules/NightPlan"

export type CinemaBooking = {
	id: string
	name: string
	client: Client
	seat: Seat
	plan: NightPlan
} 
